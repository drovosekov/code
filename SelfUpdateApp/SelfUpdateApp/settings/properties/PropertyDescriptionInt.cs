using System.Xml.Serialization;

namespace SelfUpdateApp.settings.properties
{
    [XmlRoot]
    public class PropertyDescriptionInt
    {
        public PropertyDescriptionInt()
            : this("")
        {
        }
        public PropertyDescriptionInt(string value)
        {
            Description = value;
        }

        [XmlAttribute]
        public string Description { get; set; }
        [XmlText]
        public int Value { get; set; }
    }
}