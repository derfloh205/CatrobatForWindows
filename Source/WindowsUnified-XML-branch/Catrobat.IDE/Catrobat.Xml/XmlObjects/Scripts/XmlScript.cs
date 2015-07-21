﻿using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    public abstract partial class XmlScript : XmlObjectNode
    {
        public XmlBrickList Bricks { get; set; }

        protected XmlScript()
        {
            Bricks = new XmlBrickList();
        }

        protected XmlScript(XElement xElement)
        {
            Bricks = new XmlBrickList();

            LoadFromCommonXML(xElement);
            LoadFromXml(xElement);
        }
        
        internal abstract override void LoadFromXml(XElement xRoot);

        private void LoadFromCommonXML(XElement xRoot)
        {
            XmlParserTempProjectHelper.Script = this;

            //if (xRoot.Element("brickList") != null)
            if (xRoot.Element(XmlConstants.BrickList) != null)
            {
                //Bricks = new XmlBrickList(xRoot.Element("brickList"));
                Bricks = new XmlBrickList(xRoot.Element(XmlConstants.BrickList));
            }
        }

        internal abstract override XElement CreateXml();

        protected void CreateCommonXML(XElement xRoot)
        {
            if (Bricks != null)
            {
                XmlParserTempProjectHelper.Script = this;

                xRoot.Add(Bricks.CreateXml());
            }
        }
    }
}