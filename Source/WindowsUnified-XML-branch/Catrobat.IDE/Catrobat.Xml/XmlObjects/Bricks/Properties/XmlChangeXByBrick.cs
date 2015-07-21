﻿using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeXByBrick : XmlBrick
    {
        public XmlFormula XMovement { get; set; }

        public XmlChangeXByBrick() {}

        public XmlChangeXByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            //XMovement = new XmlFormula(xRoot.Element("xMovement"));
            XMovement = new XmlFormula(xRoot.Element(XmlConstants.XPositionChange));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("changeXByNBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "changeXByNBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeXByBrickType);

            //var xVariable = new XElement("xMovement");
            var xVariable = new XElement(XmlConstants.XPositionChange);
            xVariable.Add(XMovement.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (XMovement != null)
                XMovement.LoadReference();
        }
    }
}