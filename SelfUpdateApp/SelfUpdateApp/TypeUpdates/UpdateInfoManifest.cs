using System.Collections.Generic; 
using System.Xml.Serialization;
using SelfUpdateApp.TypeUpdates.FileFormats;

namespace SelfUpdateApp.TypeUpdates
{
    [XmlRoot]
    [XmlInclude(typeof(ZipArchiveFile))]
    [XmlInclude(typeof(MsiUpdate))]
    [XmlInclude(typeof(RawFile))]
    public class UpdateInfoManifest
    {
        [XmlElement]
        public string UpdatedTimeStamp { get; set; }

        [XmlElement]
        public string UpdatedDescription { get; set; }

        [XmlElement]
        public List<ServerUpdateFile> UpdateFilesList { get; set; }
    } 
}