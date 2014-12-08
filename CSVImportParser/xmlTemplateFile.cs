using System;
using System.Xml;
using System.Linq;

namespace CSVImportParser
{
    public class xmlTemplateFile : IDisposable
    {
        private XmlDocument _xmlDoc = new XmlDocument();
        private XmlNode _ParentNode;
        private XmlNode _PrevNode;

        public xmlTemplateFile(string InfoFilePath)
        {
            if (string.IsNullOrEmpty(InfoFilePath)) { return; }
            _xmlDoc.Load(InfoFilePath);
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
                    HeaderText = x.Attributes.Count == 0 ? string.Empty : x.Attributes.GetNamedItem("HeaderText").Value,
                    DataPropertyName = x.Attributes.Count == 0 ? string.Empty : x.Attributes.GetNamedItem("RealName").Value
                })
                .ToArray();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _xmlDoc = null;
            }
        }

        public void AddNode(string TagName)
        {
            AddNode(TagName, null);
        }
        public void AddNode(string TagName, string text)
        {
            AddNode(TagName, text, false);
        }
        public void AddNode(string TagName, string text, bool SetNewAsParentNode)
        {
            XmlNode newNode = _xmlDoc.CreateNode(XmlNodeType.Element, TagName, null);
            newNode.InnerText = text;
            if (_ParentNode == null)
            {
                _ParentNode = _xmlDoc.AppendChild(newNode);
                _PrevNode = _ParentNode;
            }
            else if (SetNewAsParentNode)
            {
                _ParentNode = _ParentNode.AppendChild(newNode);
                _PrevNode = _ParentNode;
            }
            else
            {
                _PrevNode = _ParentNode.AppendChild(newNode);
            }
        }

        public void AddAttribute(string AttrName)
        {
            AddAttribute(AttrName, "");
        }
        public void AddAttribute(string AttrName, string value)
        {
            if (_PrevNode == null) _PrevNode = _xmlDoc;
            XmlAttribute newNode = _xmlDoc.CreateAttribute(AttrName);
            newNode.Value = value;
            _PrevNode.Attributes.Append(newNode);
        }

        public void Save(string InfoFilePath)
        {
            _xmlDoc.Save(InfoFilePath);
        }
    }
}
