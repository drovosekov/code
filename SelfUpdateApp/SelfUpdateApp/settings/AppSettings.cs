using System.Runtime.Serialization;
using SelfUpdateApp.Protocols;
using SelfUpdateApp.settings.properties;

namespace SelfUpdateApp.settings
{
    [DataContract]
    public class AppSettings
    { 
        private PropertyDescriptionInt _intervalCheckUpdate = new PropertyDescriptionInt("Интервал проверки обновлений в миллисекундах");
        public PropertyDescriptionInt IntervalCheckUpdate
        {
            get { return _intervalCheckUpdate; }
            set { _intervalCheckUpdate = value; }
        }
        
        [DataMember]
        public ServerProtocol ServerInfo { get; set; }
    }
}