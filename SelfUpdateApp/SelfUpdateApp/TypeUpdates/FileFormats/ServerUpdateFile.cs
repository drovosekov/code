using System;
using System.Xml.Serialization;

namespace SelfUpdateApp.TypeUpdates.FileFormats
{
    [XmlRoot]
    public abstract class ServerUpdateFile : IComparable<ServerUpdateFile>
    {
        [XmlElement]
        public virtual string FileNameOnServer { get; set; }

        [XmlElement]
        public abstract DateTime FileCreationDateTime { get; set; }

        [XmlElement]
        public abstract string Hash { get; set; }

        [XmlIgnore]
        public string ErrorMessage { get; protected set; }

        public abstract bool Install();

        public static string UpdateFolderPath { get; set; }
        
        public int CompareTo(ServerUpdateFile other)
        { 
            if (other is MsiUpdate) return 1;
            if (other is RawFile) return -1;
            return 0;
        }

        public override string ToString()
        {
            return FileNameOnServer;
        }
    }
}