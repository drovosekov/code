using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using SelfUpdateApp.settings;

namespace SelfUpdateApp.Protocols
{
    [DataContract]
    public class SMBServer : ServerProtocol
    {
        [DataMember]
        public override DateTime FileCreationDateTime
        {
            get
            {
                var filePath = Path.Combine(ServerName.Value, ServerFileAdress.Value);
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
            set { }
        }

        public override bool DownloadFile(string fullFileName)
        {
            try
            {
                var filePath = Path.Combine(ServerName.Value, ServerFileAdress.Value);
                using (NetworkShareAccesser.Access(ServerName.Value, ServerLogin.Value,ServerPassword.Value.DecryptString()))
                {
                    File.Copy(filePath, fullFileName);
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public SMBServer() { }//: this(string.Empty) { }

        //public SMBServer(string serverName)
        //{
        //    ServerName.Value = serverName;
        //}
    }

}