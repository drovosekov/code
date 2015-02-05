using System;
using System.IO;

namespace SelfUpdateApp.TypeUpdates.FileFormats
{
    public class MsiUpdate : ServerUpdateFile
    {
        public MsiUpdate(){ }
        
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