using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SelfUpdateApp.TypeUpdates
{
    [DataContract]
    public abstract class ServerUpdateFile : IComparable<ServerUpdateFile>
    {
        [DataMember]
        public virtual string FileNameOnServer { get; set; }

        [DataMember]
        public abstract DateTime FileCreationDateTime { get; set; }

        [DataMember]
        public abstract string Hash { get; set; }

        public abstract bool Install();

        [XmlIgnore, IgnoreDataMember]
        public string ErrorMessage { get; protected set; }

        public static string UpdateFolderPath { get; set; }

        protected ServerUpdateFile(string fullFileName)
        {
            FileNameOnServer = fullFileName;
        }

        //public int CompareTo(object obj)
        //{
        //    var compared = (ServerUpdateFile)obj;
        //    return (Hash == compared.Hash) ? 1 : 0;
        //}

        public int CompareTo(ServerUpdateFile other)
        { 
            if (other.GetType() == typeof(MsiUpdate)) return 1;
            if (other.GetType() == typeof(RawFile)) return -1;
            return 0;
        }

        public override string ToString()
        {
            return FileNameOnServer;
        }
    }
}