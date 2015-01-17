using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using SelfUpdateApp.Protocols;
using SelfUpdateApp.settings;
using SelfUpdateApp.TypeUpdates;
using setings = SelfUpdateApp.settings.SettingsController;

namespace SelfUpdateApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var sett = new SettingsController(CommonFunctions.GetSettingsFilePath, new SMBServer())
            {
                IntervalCheckUpdate = 5000,
                ServerFileAdress = @"обмен\UpdateInfo.xml",
                ServerLogin = @"Dev-vs13\user",
                ServerPassword = "123",
                ServerName = @"\\Dev-vs13"
            };
            //sett.Save();
            //sett.Save();

            sett.Server.DownloadFile(CommonFunctions.GetAppUpdatePath + "UpdateInfo.xml");

            var file = new ExistingFile(CommonFunctions.GetAppFullPath);

            var list = new OnServerFilesList
            {
                FilesList = new List<ServerUpdateFile> { file },
                UpdatedTimeStamp = String.Format("{0} - {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString()),
                UpdateUrl = @"C:\update\"
            };

            var xmlSerializer = new XmlSerializer(typeof(OnServerFilesList));

            using (var ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, list);
                byte[] wayPoints = ms.GetBuffer();

                wayPoints = wayPoints.Where(x => x != 0).ToArray(); //убираем лишние нули взятые из буфера

                using (FileStream fs = File.Open(file.FullFileName + ".xml", FileMode.Create))
                {
                    fs.Write(wayPoints, 0, wayPoints.Length);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormApp());
        }
    }
}
