using System.Xml.Serialization;

namespace SelfUpdateApp.settings.properties
{
    [XmlRoot ]
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

        [XmlAttribute]
        public string Description { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}