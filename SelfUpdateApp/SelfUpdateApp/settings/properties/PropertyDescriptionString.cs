using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SelfUpdateApp.settings.properties
{
    [DataContract]
    public class PropertyDescriptionString
    {
        public PropertyDescriptionString()
            : this("")
        {
        }
        public PropertyDescriptionString(string desc)
        {
            Description = desc;
        }
        public PropertyDescriptionString(string desc, string value)
        {
            Description = desc;
            Value = value;
        }

        [DataMember, XmlAttribute]
        public string Description { get; set; }
        [DataMember, XmlText]
        public string Value { get; set; }
    }
}