﻿using System;
using System.IO;
using SelfUpdateApp.settings;

namespace SelfUpdateApp.TypeUpdates
{
    public class ZipArchiveFile : ServerUpdateFile
    {

        public ZipArchiveFile()
            : this("") { }

        public ZipArchiveFile(string fileName)
            : base(fileName) { }

        public override bool Install()
        {
            throw new NotImplementedException();
        }

        private string _fileNameOnServer;
        public override string FileNameOnServer
        {
            get { return base.FileNameOnServer; }
            set
            {
                base.FileNameOnServer = value.Replace(CommonFunctions.GetAppDirectoryPath + @"\", "");
                _fileNameOnServer = value;
            }
        }

        public override string Hash
        {
            get
            {
                try
                {
                    using (FileStream fs = File.Open(_fileNameOnServer, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
            set { }
        }

        public override DateTime FileCreationDateTime
        {
            get { return File.GetCreationTimeUtc(_fileNameOnServer); }
            set { }
        }
    }
}