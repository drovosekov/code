using System;
using System.Xml;
using System.Linq;

namespace CSVImportParser
{
    public sealed class XmlTemplateFile : IDisposable
    {
        private XmlDocument _xmlDoc = new XmlDocument();
        private XmlNode _parentNode;
        private XmlNode _prevNode;

        public XmlTemplateFile(string infoFilePath)
        {
            if (string.IsNullOrEmpty(infoFilePath)) { return; }
            _xmlDoc.Load(infoFilePath);
        }

        public string GetByTag(string tag)
        {
            return _xmlDoc.GetElementsByTagName(tag)[0].InnerText;
        }

        public ColumnInfo[] GetNodesListToArrayByTag(string tag)
        {
            return _xmlDoc.GetElementsByTagName(tag)
                .Cast<XmlNode>()
                .Select(x => new ColumnInfo()
                {
                    HeaderText = (x.Attributes != null && x.Attributes.Count == 0) ? string.Empty : x.Attributes.GetNamedItem("HeaderText").Value,
                    DataPropertyName = x.Attributes.Count == 0 ? string.Empty : x.Attributes.GetNamedItem("RealName").Value
                })
                .ToArray();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _xmlDoc = null;
            }
        }

        public void AddNode(string tagName, string text = "", bool setNewAsParentNode = false)
        {
            XmlNode newNode = _xmlDoc.CreateNode(XmlNodeType.Element, tagName, null);
            newNode.InnerText = text;
            if (_parentNode == null)
            {
                _parentNode = _xmlDoc.AppendChild(newNode);
                _prevNode = _parentNode;
            }
            else if (setNewAsParentNode)
            {
                _parentNode = _parentNode.AppendChild(newNode);
                _prevNode = _parentNode;
            }
            else
            {
                _prevNode = _parentNode.AppendChild(newNode);
            }
        }

        public void AddAttribute(string attrName, string value = "")
        {
            if (_prevNode == null) _prevNode = _xmlDoc;
            XmlAttribute newNode = _xmlDoc.CreateAttribute(attrName);
            newNode.Value = value;
            if (_prevNode.Attributes != null) _prevNode.Attributes.Append(newNode);
        }

        public void Save(string infoFilePath)
        {
            _xmlDoc.Save(infoFilePath);
        }
    }
}
