using System;
using System.IO; 
using System.Xml.Serialization;
using SelfUpdateApp.settings;

namespace SelfUpdateApp.Protocols
{
    [XmlRoot]
    public class SmbServer : ServerProtocol
    {
        [XmlIgnore]
        public override DateTime FileOnServerCreationDateTime
        {
            get
            {
                var filePath = Path.Combine(ServerName.Value, ShareName.Value, ServerFileAdress.Value);
                try
                {
                    using (NetworkShareAccesser.Access(ServerName.Value, ServerLogin.Value, ServerPassword.Value.DecryptString()))
                    {
                        return File.GetLastWriteTimeUtc(filePath);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ErrorMessage = ex.Message;
                }
                return DateTime.MinValue;
            }
        }

        public override bool DownloadFileTo(string destinationFullFileName)
        {
            try
            {
                var filePath = Path.Combine(ServerName.Value, ShareName.Value, ServerFileAdress.Value);
                using (NetworkShareAccesser.Access(ServerName.Value, ServerLogin.Value, ServerPassword.Value.DecryptString()))
                {
                    File.Copy(filePath, destinationFullFileName, true);
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public SmbServer() { }//: this(string.Empty) { }

        //public SMBServer(string serverName)
        //{
        //    ServerName.Value = serverName;
        //}
    }

}