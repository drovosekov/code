using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;
using SelfUpdateApp.Protocols;

namespace SelfUpdateApp.settings
{
    public class SettingsController
    {
        private AppSettings Properties { get; set; }

        public SettingsController()
            : this(string.Empty , null)
        {
            //пустой конструктор без параметров нужен для серилизации
        }
        public SettingsController(string settingsFilePath, ServerProtocol typeServer = null)
        {
            if (string.IsNullOrEmpty(settingsFilePath)) return;
            SettingsFilePath = settingsFilePath;

            if (!File.Exists(settingsFilePath))
            {
                Properties = new AppSettings();
                Server = typeServer;
                return;
            }

            using (FileStream fs = File.Open(SettingsFilePath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                var xmlSerializer = new XmlSerializer(typeof(AppSettings));
                using (Stream stream = new MemoryStream(buffer))
                {
                    Properties = (AppSettings)xmlSerializer.Deserialize(stream);
                }
            }
        }

        public void Save()
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    var xmlSerializer = new XmlSerializer(typeof(AppSettings));
                    xmlSerializer.Serialize(ms, Properties);
                    byte[] wayPoints = ms.GetBuffer()
                        .Where(x => x != 0) //убираем лишние нули взятые из буфера
                        .ToArray();

                    using (FileStream fs = File.Open(SettingsFilePath, FileMode.Create))
                    {
                        fs.Write(wayPoints, 0, wayPoints.Length);
                    }
                }
            }
            catch (SerializationException sex)
            {
                MessageBox.Show(sex.StackTrace, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception sex)
            {
                MessageBox.Show(sex.InnerException.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private static string SettingsFilePath { get; set; }


        #region параметры настроек
        public ServerProtocol Server
        {
            get
            {
                if (Properties.ServerInfo == null) throw new Exception("Не задан тип сервера обновлений");
                return Properties.ServerInfo;
            }
            set { Properties.ServerInfo = value; }
        }

        public int IntervalCheckUpdate
        {
            get { return Properties.IntervalCheckUpdate.Value; }
            set { Properties.IntervalCheckUpdate.Value = value; }
        }

        public string ServerName
        {
            get { return Properties.ServerInfo.ServerName.Value; }
            set { Properties.ServerInfo.ServerName.Value = value; }
        }

        public string ServerFileAdress
        {
            get { return Properties.ServerInfo.ServerFileAdress.Value; }
            set { Properties.ServerInfo.ServerFileAdress.Value = value; }
        }

        public string ServerLogin
        {
            get { return Properties.ServerInfo.ServerLogin.Value; }
            set { Properties.ServerInfo.ServerLogin.Value = value; }
        }

        public string ServerPassword
        {
            get { return Properties.ServerInfo.ServerPassword.Value.DecryptString(); }
            set { Properties.ServerInfo.ServerPassword.Value = value.EncryptString(); }
        }

        #endregion

    }
}