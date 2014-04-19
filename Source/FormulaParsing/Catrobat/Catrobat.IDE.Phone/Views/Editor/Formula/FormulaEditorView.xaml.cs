﻿using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Editor.Formula;
using Catrobat.IDE.Phone.Controls.FormulaControls;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Catrobat.IDE.Phone.Views.Editor.Formula
{
    public partial class FormulaEditorView
    {
        private readonly FormulaEditorViewModel _viewModel = ServiceLocator.ViewModelLocator.FormulaEditorViewModel;

        public FormulaEditorView()
        {
            InitializeComponent();

            FormulaKeyboard.KeyPressed += KeyPressed;
            FormulaKeyboard.EvaluatePressed += EvaluatePressed;
            _viewModel.ErrorOccurred += ErrorOccurred;
            _viewModel.Evaluated += Evaluated;
        }

        #region Transition animations

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
                var animation = FormulaKeyboard.Resources["EnterTransition"] as Storyboard;
            if (animation != null)
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1));
                animation.Begin();
            }
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
            {
                var animation = FormulaKeyboard.Resources["ExitTransition"] as Storyboard;
            if (animation != null)
            {
                animation.Begin();
                await Task.Delay(TimeSpan.FromSeconds(0.3));
            }
            base.OnNavigatingFrom(e);
        }

        #endregion

        private void ShowKeyErrorAnimation()
        {
            KeyErrorAnimation.Stop();
            KeyErrorAnimation.Begin();
        }

        private void ErrorOccurred()
        {
            ShowKeyErrorAnimation();
        }

        private void KeyPressed(FormulaKeyEventArgs e)
        {
            _viewModel.KeyPressedCommand.Execute(e);
        }

        private void EvaluatePressed()
        {
            _viewModel.EvaluatePressedCommand.Execute(null);
        }

        private void Evaluated(object value)
        {
            // TODO: pretty up toast notification
            var message = value == null ? string.Empty : value.ToString();
            ServiceLocator.NotifictionService.ShowToastNotification("", message, ToastNotificationTime.Medeum);
        }

        private bool _firstBackPressed = true;
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (_viewModel.HasError && _firstBackPressed)
            {
                e.Cancel = true;
                _firstBackPressed = false;

                var timeToAct = TimeSpan.FromSeconds(1);
                ServiceLocator.NotifictionService.ShowToastNotification(
                    title: "",
                    message: AppResources.Editor_ReallyDismissFormula,
                    timeTillHide: timeToAct);
                Task.Run(async () =>
                {
                    await Task.Delay(timeToAct);
                    _firstBackPressed = true;
                });
            }
            else
            {
                _viewModel.GoBackCommand.Execute(null);
                e.Cancel = true;
                base.OnBackKeyPress(e);
            }
        }

        private void FormulaEditorView_OnUnloaded(object sender, RoutedEventArgs e)
        {
            // XAML bindings are not removed themselves! hopefully this changes in Windows Phone 8.1
            FormulaViewer.ClearValue(FormulaViewer3.TokensProperty);
            FormulaViewer.ClearValue(FormulaViewer3.CaretIndexProperty);
            FormulaViewer.ClearValue(FormulaViewer3.SelectionLengthProperty);
            FormulaKeyboard.ClearValue(FormulaKeyboard.CanDeleteProperty);
            FormulaKeyboard.ClearValue(FormulaKeyboard.CanUndoProperty);
            FormulaKeyboard.ClearValue(FormulaKeyboard.CanRedoProperty);
            FormulaKeyboard.ClearValue(FormulaKeyboard.CanLeftProperty);
            FormulaKeyboard.ClearValue(FormulaKeyboard.CanRightProperty);
            FormulaKeyboard.ClearValue(FormulaKeyboard.CanEvaluateProperty);
            FormulaKeyboard.ClearValue(FormulaKeyboard.ParsingErrorProperty);

            _viewModel.ErrorOccurred -= ErrorOccurred;
            _viewModel.Evaluated -= Evaluated;

            _viewModel.Cleanup();
        }

        private void ApplicationBarMenuItemStart_OnClick(object sender, EventArgs e)
        {
            _viewModel.StartSensorsCommand.Execute(null);
        }
        private void ApplicationBarMenuItemStop_OnClick(object sender, EventArgs e)
        {
            _viewModel.StopSensorsCommand.Execute(null);
        }
    }
}