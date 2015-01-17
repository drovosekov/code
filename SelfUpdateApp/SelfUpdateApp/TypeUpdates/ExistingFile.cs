using System;
using System.IO;
using System.Runtime.Serialization;
using SelfUpdateApp.settings;

namespace SelfUpdateApp.TypeUpdates
{
    public class ExistingFile : ServerUpdateFile
    {
        public ExistingFile()
            : this("")
        {
        }
        public ExistingFile(string fullFileName)
            : base(fullFileName)
        {
        }
         
        public override string Hash
        {
            get
            {
                try
                {
                    using (FileStream fs = File.Open(FullFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        return fs.GetHashOfStream();
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return string.Empty;
                }
            }
            set { throw new NotImplementedException(); }
        }
         
        public override DateTime FileCreationDateTime
        {
            get { return File.GetCreationTime(FullFileName); }
            set { throw new NotImplementedException(); }
        }
    }
}