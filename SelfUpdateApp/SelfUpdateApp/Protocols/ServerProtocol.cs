using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SelfUpdateApp.settings.properties;

namespace SelfUpdateApp.Protocols
{
    [DataContract]
    [KnownType(typeof(RestServer)), KnownType(typeof(SMBServer))]
    [XmlInclude(typeof(RestServer)), XmlInclude(typeof(SMBServer))]
    public abstract class ServerProtocol : IGetFile
    {
        private static PropertyDescriptionString _serverName = new PropertyDescriptionString("Адрес сервера обновлений");
        public PropertyDescriptionString ServerName
        {
            get { return _serverName; }
            set { _serverName = value; }
        }
        
        private PropertyDescriptionString _serverFileAdress = new PropertyDescriptionString("Путь к файлу на сервере");
        public PropertyDescriptionString ServerFileAdress
        {
            get { return _serverFileAdress; }
            set { _serverFileAdress = value; }
        }

        private static PropertyDescriptionString _serverLogin = new PropertyDescriptionString("Логин для доступа к серверу");
        public PropertyDescriptionString ServerLogin
        {
            get { return _serverLogin; }
            set { _serverLogin = value; }
        }

        private static PropertyDescriptionString _serverPassword = new PropertyDescriptionString("Пароль для доступа к серверу");
        public PropertyDescriptionString ServerPassword
        {
            get { return _serverPassword; }
            set { _serverPassword = value; }
        }

        public abstract bool DownloadFile(string destinationFullFileName);

        [DataMember]
        public abstract DateTime FileCreationDateTime { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }
}