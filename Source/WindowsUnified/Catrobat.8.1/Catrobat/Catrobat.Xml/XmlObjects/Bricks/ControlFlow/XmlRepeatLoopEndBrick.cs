﻿using Catrobat_Player.NativeComponent;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlRepeatLoopEndBrick : XmlLoopEndBrick, IRepeatEndBrick
    {
        public XmlRepeatLoopEndBrick() { }

        public XmlRepeatLoopEndBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                base.LoadFromCommonXML(xRoot);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlRepeatLoopEndBrickType);
            base.CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}
