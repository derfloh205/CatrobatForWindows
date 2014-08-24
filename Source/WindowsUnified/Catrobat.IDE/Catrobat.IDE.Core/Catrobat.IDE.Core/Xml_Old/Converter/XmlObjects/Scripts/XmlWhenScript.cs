﻿using System;
using Catrobat.IDE.Core.Models.Scripts;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    partial class XmlWhenScript
    {
        protected internal override Script ToModel2(Context context)
        {
            switch (Action)
            {
                case WhenScriptAction.Tapped: return new TappedScript();
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}