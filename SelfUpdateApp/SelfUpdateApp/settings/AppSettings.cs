using System.Xml.Serialization;
using SelfUpdateApp.Protocols;
using SelfUpdateApp.settings.properties;

namespace SelfUpdateApp.settings
{
    [XmlRoot]
    public class AppSettings
    {
        private PropertyDescriptionInt _intervalCheckUpdate = new PropertyDescriptionInt("Интервал проверки обновлений в миллисекундах");
        
        [XmlElement]
        public PropertyDescriptionInt IntervalCheckUpdate
        {
            get { return _intervalCheckUpdate; }
            set { _intervalCheckUpdate = value; }
        }

        [XmlElement]
        public ServerProtocol ServerInfo { get; set; }
    }
}