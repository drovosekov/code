using System;
using System.Xml;

namespace SelfUpdateApp
{
    public class xmlInfoFile : IDisposable
    {
        private XmlDocument xmlInfoFile = new XmlDocument();

        public xmlInfoFile(string InfoFilePath)
        {
            xmlInfoFile.Load(InfoFilePath);
        }

        public XmlNodeList  GetByTag(string tag){
            return xmlInfoFile.GetElementsByTagName(tag);
        }

        public void Dispose()
        {
            xmlInfoFile = null;
        }
    }
}
