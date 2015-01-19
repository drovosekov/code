using System;
using System.IO;

namespace SelfUpdateApp.TypeUpdates
{
    public class MsiUpdate : ServerUpdateFile
    {
        public MsiUpdate()
            : this(string.Empty) { }

        public MsiUpdate(string fileName)
            : base(fileName) { }

        public override bool Install()
        {
            FileNameOnServer = FileNameOnServer;
            return true;
        }

        public override DateTime FileCreationDateTime
        {
            get { return File.GetCreationTimeUtc(FileNameOnServer); }
            set { }
        }

        public override string Hash { get; set; }
    }
}