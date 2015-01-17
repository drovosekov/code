using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SelfUpdateApp.settings.properties
{
    [DataContract]
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

        [DataMember, XmlAttribute]
        public string Description { get; set; }
        [DataMember, XmlText]
        public int Value { get; set; }
    }
}