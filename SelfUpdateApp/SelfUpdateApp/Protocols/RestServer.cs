using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace SelfUpdateApp.Protocols
{
    [DataContract]
    public class RestServer : ServerProtocol
    {
        public override DateTime FileCreationDateTime
        {
            get
            {
                var request = (HttpWebRequest)WebRequest.Create(ServerFileAdress.Value);

                try
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        return Convert.ToDateTime(response.Headers["Last-Modified"]);
                    }
                }
                catch (WebException wex)
                {
                    MessageBox.Show(wex.Message);
                    return DateTime.MinValue;
                }
            }
            set { }
        }

        public override  bool DownloadFile(string fullFileName)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var localDirectoryPath = Path.GetDirectoryName(fullFileName);
                    if (!Directory.Exists(fullFileName) && localDirectoryPath != null)
                    {
                        Directory.CreateDirectory(localDirectoryPath);
                    }
                    if (File.Exists(fullFileName)) { File.Delete(fullFileName); }
                    client.DownloadFile(new Uri(ServerFileAdress.Value), fullFileName);
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public RestServer() { }// : this(string.Empty) { }

        //public RestServer(string serverFileAdress)
        //{
        //    ServerFileAdress.Value = serverFileAdress;
        //}
         
    }
}