using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SelfUpdateApp.TypeUpdates
{
    [DataContract]
    [XmlInclude(typeof(ZipArchiveFile)), KnownType(typeof(ZipArchiveFile))]
    [XmlInclude(typeof(MsiUpdate)), KnownType(typeof(MsiUpdate))]
    [XmlInclude(typeof(RawFile)), KnownType(typeof(RawFile))]
    public class UpdateInfoManifest
    {
        [DataMember]
        public string UpdatedTimeStamp { get; set; }
        
        [DataMember]
        public List<ServerUpdateFile> UpdateFilesList { get; set; }
    } 
}