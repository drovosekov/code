using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using SelfUpdateApp.settings;

namespace SelfUpdateApp
{
    public class AppUpdater
    {
        public string ErrorMessage;
        public string UpdateInfoFileName;
        public string ServerAddr;
        public bool CheckOnlyHash; 
        //массив задач на ассинхронное скачивание файлов с сервера обновлений
        private Task[] _ts = new Task[0];

        /// <summary>
        /// если в файле настроек ключ CheckOnlyHash=true - то все файлы сравниваются только по хешу (более надежно)
        /// в не зависимости от этойнастройки - все файлы, у которых нельзя проверить версию файла (например текстовые)
        /// сравниваются только по изменению хеша файла
        /// </summary>
        
        public bool Check()
        {
            Task.Factory.StartNew(() =>
                //скачиваем файл описания обновлений (версия файла, имя файла, контр. сумма)
                 DownloadFile(UpdateInfoFileName)
            ).Wait();

            var infoFilePath = Path.Combine(CommonFunctions.GetAppUpdatePath, UpdateInfoFileName);
            if (!File.Exists(infoFilePath)) return false;

            /*using (var info = new XmlInfoFile(infoFilePath))
            {
                foreach (XmlNode xmlNode in info.GetByTag("Element"))
                {
                    var fileName = xmlNode.SelectSingleNode("FileName").InnerText;
                    var filePath = Path.Combine(CommonFunctions.GetAppFullPath, fileName);

                    string updateFilePath;
                    if (!File.Exists(filePath))
                    {
                        updateFilePath = Path.Combine(CommonFunctions.GetAppUpdatePath, fileName);
                        if (!File.Exists(updateFilePath))
                        {
                            //файла нет вообще - качаем его
                            NewDownloadTask(fileName);
                        }
                        else
                        {
                            //файл уже скачан и ждет обновления 
                            //если он отличается от серверного - качаем снова
                            if (CheckFile(xmlNode, updateFilePath))
                            {
                                NewDownloadTask(fileName);
                            }
                        }
                    }
                    else
                    {
                        updateFilePath = Path.Combine(CommonFunctions.GetAppUpdatePath, fileName);
                        if (File.Exists(updateFilePath))
                        {
                            //файл уже скачан и ждет обновления 
                            //если он отличается от серверного - качаем снова
                            if (CheckFile(xmlNode, updateFilePath))
                            {
                                NewDownloadTask(fileName);
                            }
                        }
                        else
                        {
                            //если после проверки локального фала его хеш или версия отличаются
                            //от записанной в файле описания обновлений - качаем в папку обновлений
                            if (CheckFile(xmlNode, filePath))
                            {
                                NewDownloadTask(fileName);
                            }
                        }
                    }
                }
                if (_ts.Length <= 0) return false;
                //ждем завершения всех заданий скачивания файлов
                Task.WaitAll(_ts);
                return true;
            }*/

            return false;
        }

        /// <summary>
        /// проверяем файл на сервере с локальным файлом по версии файла или хешу файла
        /// </summary>
        /// <param name="xmlNode">ветка в файле описания обновлений содежащия инфо о версии и хеше файла</param>
        /// <param name="filePath">путь к файлу версию которого надо проверить</param> 
        /// <returns>в случае отличия или с меньшей версией локального файла от серверного - true</returns>
        private bool CheckFile(XmlNode xmlNode, string filePath)
        {
            var hash = CommonFunctions.GetHashFile(filePath);
            
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filePath);

            if (versionInfo.FileVersion == null || CheckOnlyHash)
            {
                //для не ехе, dll файлов проверяем хеш файла
                XmlNode selectSingleNode = xmlNode.SelectSingleNode("HASH");
                if (selectSingleNode != null && hash != selectSingleNode.InnerText)
                {
                    return true;
                }
            }
            else
            {
                //у всех ехе, dll файлов проверяем фактическую версию
                var fileVersion = new Version(versionInfo.FileVersion);
                XmlNode selectSingleNode = xmlNode.SelectSingleNode("Version");
                if (selectSingleNode == null) return false;

                var remoteVersion = new Version(selectSingleNode.InnerText);
                if (remoteVersion > fileVersion) return true;
            }
            return false;
        }

        /// <summary>
        /// создаем новую задачу на скачивание файла
        /// </summary>
        /// <param name="fileName">путь к скачиваемому с сревера обновлений файла</param> 
        private void NewDownloadTask(string fileName)
        {
            Array.Resize(ref _ts, _ts.Length + 1);
            _ts[_ts.Length - 1] = Task.Factory.StartNew(() =>
                DownloadFile(fileName)
            );
        }

        private void DownloadFile(string fileNameToDownload)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var localFilePath = String.Format("{0}{1}", CommonFunctions.GetAppUpdatePath, fileNameToDownload);
                    if (!Directory.Exists(localFilePath)) { Directory.CreateDirectory(Path.GetDirectoryName(localFilePath)); }
                    if (File.Exists(localFilePath)) { File.Delete(localFilePath); }
                    client.DownloadFile(new Uri(String.Format("http://{0}/{1}", ServerAddr, fileNameToDownload)),
                                        localFilePath);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
