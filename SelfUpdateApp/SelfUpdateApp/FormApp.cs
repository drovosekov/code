using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text;
using SelfUpdateApp.settings;
using SelfUpdateApp.TypeUpdates;

namespace SelfUpdateApp
{
    public partial class FormApp : Form
    {
        private readonly SettingsController _appSettings = new SettingsController(CommonFunctions.GetSettingsFilePath);
        private AppUpdater _appUpdater;
        public FormApp()
        {
            InitializeComponent();
        }

        private void TMR_CheckUpdate_Tick(object sender, EventArgs e)
        {
            if (_appUpdater != null) return;

            try
            {
                tmrCheckUpdate.Enabled = cmdUpdate.Enabled = false;
                lblCheckUpdates.Text = @"Проверка обновлений...";

                _appUpdater = new AppUpdater(_appSettings.Server);
                _appUpdater.Check(CheckUpdateResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                tmrCheckUpdate.Enabled = true;
                _appUpdater = null;
            }
        }

        private void CheckUpdateResult(bool result)
        {
            cmdUpdate.Enabled = result;
            lblCheckUpdates.Text = result ? "Есть обновления" : "Нет обновлений";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ////для индикации работы интерфейса
            lblTicks.Text = DateTime.Now.Ticks.ToString();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ServerUpdateFile.UpdateFolderPath = CommonFunctions.GetAppUpdateDirectoryPath;

            var thisVersion = new Version(Application.ProductVersion);
            lblCurrentVersion.Text += thisVersion;

            tmrCheckUpdate.Interval = (_appSettings.IntervalCheckUpdate == 0) ?
                                       300000 :
                                       _appSettings.IntervalCheckUpdate;
            lblCheckUpdatesInterval.Text += tmrCheckUpdate.Interval / 1000;
            ////проверка обновления при запуске
            TMR_CheckUpdate_Tick(null, null);
        }

        private void CMD_Update_Click(object sender, EventArgs e)
        {
            #region проверки и предупреждения
            var proc = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(proc);
            if (processes.Length > 1)
            {
                MessageBox.Show(@"Закройте все запущенные экземпляры программы, кроме текущего",
                                @"Подготовка к обновлению", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show(@"В процессе обновления программа будет перезапущена.\nПродолжить?",
                                @"Подготовка к обновлению", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            #endregion

            #region скрипт обновления файлов
            ////ждем 3 секунды 
            var cmdTextBuilder = new StringBuilder("TIMEOUT 3\r\n");
            ////удаляем инфо-файл
            cmdTextBuilder.AppendFormat("DEL /Q /F \"{0}UpdateInfo.xml\"\r\n", CommonFunctions.GetAppUpdateDirectoryPath);
            ////перемещаем все скаченные файлы из папки обновлений
            cmdTextBuilder.AppendFormat("MOVE /Y \"{0}*\" \"{1}\"\r\n", CommonFunctions.GetAppUpdateDirectoryPath, CommonFunctions.GetAppFullPath);
            ////запускаем программу
            cmdTextBuilder.AppendFormat("START \"title\" \"{0}\"\r\n", CommonFunctions.GetAppFullPath);
            ////удаляем скрипт обновления
            cmdTextBuilder.AppendFormat("DEL /Q /F \"{0}update.cmd\"\r\n", CommonFunctions.GetAppFullPath);

            proc = Path.Combine(CommonFunctions.GetAppFullPath, "update.cmd");

            using (var fs = new FileStream(proc, FileMode.Create))
            {
                using (var writer = new StreamWriter(fs, Encoding.GetEncoding(866)))
                {
                    writer.Write(cmdTextBuilder.ToString());
                }
            }
            #endregion

            #region скрытный запуск скрипта
            Process.Start(new ProcessStartInfo()
            {
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = String.Format("\"{0}\"", proc),
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            });
            #endregion

            Close();
        }
    }
}
