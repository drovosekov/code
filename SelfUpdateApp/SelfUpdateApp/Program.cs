﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using SelfUpdateApp.Protocols;
using SelfUpdateApp.settings;
using SelfUpdateApp.TypeUpdates;

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
                #region создвание файла настроек

                var sett = new SettingsController(CommonFunctions.GetSettingsFilePath, new SmbServer())
                {
                    IntervalCheckUpdate = 5000,
                    ServerLogin = @"Dev-vs13\user",
                    ServerPassword = "123",
                    ServerName = @"\\Dev-vs13",
                    ServerFileAdress = @"UpdateInfo.xml",
                    ShareName = "обмен"
                };
                sett.Save();
                //sett.Save();

                sett.Server.DownloadFileTo(CommonFunctions.GetAppLocalUpdateInfoFilePath);

                #endregion

            if (false)
            {
                #region создание файла описания обновлений

                var file = new RawFile(CommonFunctions.GetAppFullPath);

                var list = new UpdateInfoManifest
                {
                    UpdateFilesList = new List<ServerUpdateFile> {file},
                    UpdatedTimeStamp =
                        String.Format("{0} - {1}", DateTime.UtcNow.ToLongDateString(),
                            DateTime.UtcNow.ToLongTimeString())
                };

                var xmlSerializer = new XmlSerializer(typeof (UpdateInfoManifest));

                using (var ms = new MemoryStream())
                {
                    xmlSerializer.Serialize(ms, list);
                    byte[] wayPoints = ms.GetBuffer();

                    wayPoints = wayPoints
                        .Where(x => x != 0)
                        .AsParallel()
                        .ToArray(); //убираем лишние нули взятые из буфера

                    using (FileStream fs = File.Open(CommonFunctions.GetAppLocalUpdateInfoFilePath, FileMode.Create))
                    {
                        fs.Write(wayPoints, 0, wayPoints.Length);
                    }
                }

                #endregion
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormApp());
        }
    }
}
