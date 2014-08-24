﻿using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlChangeGhostEffectBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new ChangeTransparencyBrick
            {
                RelativePercentage = ChangeGhostEffect == null ? null : ChangeGhostEffect.ToModel(context)
            };
        }
    }
}