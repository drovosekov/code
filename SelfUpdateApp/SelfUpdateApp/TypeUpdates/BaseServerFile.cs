using System;
using System.Runtime.Serialization;

namespace SelfUpdateApp.TypeUpdates
{
    [DataContract]
    public abstract class ServerUpdateFile : IComparable
    {
        [DataMember]
        protected string ServerFileAdress { get; set; }
        
        [DataMember]
        public string FullFileName { get; set; }

        [DataMember]
        public abstract DateTime FileCreationDateTime { get; set; }

        [DataMember]
        public abstract string Hash { get; set; }

        [DataMember]
        public virtual string FileVersion { get; set; }

        protected string ErrorMessage { get; set; }

        protected ServerUpdateFile(string fullFileName)
        {
            FullFileName = fullFileName;
        }

        public int CompareTo(object obj)
        {
            var compared = (ServerUpdateFile)obj;
            return (Hash == compared.Hash || FileVersion == compared.FileVersion) ? 1 : 0;
        }

        public override string ToString()
        {
            return FullFileName;
        }
    }
}