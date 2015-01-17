using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SelfUpdateApp.TypeUpdates
{
    [DataContract]
    [KnownType(typeof(ExistingFile)), KnownType(typeof(MSIUpdate))]
    [XmlInclude(typeof(ExistingFile)), XmlInclude(typeof(MSIUpdate))]
    public class OnServerFilesList
    {
        [DataMember]
        public string UpdatedTimeStamp { get; set; }
        
        [DataMember]
        public string UpdateUrl { get; set; }

        [DataMember]
        public List<ServerUpdateFile> FilesList { get; set; }
    } 
}