using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Serialization;
using SelfUpdateApp.Protocols;
using SelfUpdateApp.settings;
using SelfUpdateApp.TypeUpdates;
using SelfUpdateApp.TypeUpdates.FileFormats;

namespace SelfUpdateApp
{
    public class AppUpdater : IDisposable
    {
        public string ErrorMessage;
        private UpdateInfoManifest _filesForUpdateInfo;
        private ServerProtocol _serverProtocol;
        private readonly string _localInfoFilePath = CommonFunctions.GetAppLocalUpdateInfoFilePath;

        public AppUpdater(ServerProtocol infoUpdateFile)
        {
            _serverProtocol = infoUpdateFile;
        }

        /// <summary>
        /// 
        /// </summary>

        public async void Check(Action<bool> checkResult)
        {
            if (checkResult == null) throw new Exception("Не задан делегат CheckResult");

            if (File.Exists(_localInfoFilePath))
            {
                DateTime fileCreationTime = _serverProtocol.FileOnServerCreationDateTime;
                //if (DateTime.Compare(fileCreationTime, File.GetLastWriteTimeUtc(_localInfoFilePath)) <= 0)  {
                checkResult(false);
                return;
                //}
            }

            if (await Task<bool>.Factory
                        .StartNew(() => _serverProtocol.DownloadFileTo(_localInfoFilePath)) == false)
            {
                Log(_serverProtocol.ErrorMessage);
                checkResult(false);
                return;
            };

            try
            {
                using (FileStream fs = File.Open(_localInfoFilePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                    var xmlSerializer = new XmlSerializer(typeof(UpdateInfoManifest));
                    using (Stream stream = new MemoryStream(buffer))
                    {
                        _filesForUpdateInfo = (UpdateInfoManifest)xmlSerializer.Deserialize(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                checkResult(false);
                return;
            }

            checkResult(_filesForUpdateInfo.UpdateFilesList.Count > 0);

            //switch (_filesForUpdateInfo.UpdateFilesList.Count)
            //{
            //    case 0:
            //        checkResult(false);
            //        return;
            //    case 1:
            //        DownloadFileForUpdate(_filesForUpdateInfo.UpdateFilesList[0]);
            //        break;
            //    default:
            //        _filesForUpdateInfo.UpdateFilesList.Sort();
            //        Parallel.ForEach(_filesForUpdateInfo.UpdateFilesList, DownloadFileForUpdate);
            //        break;
            //}
        }

        /// <summary>
        /// проверяем файл на сервере с локальным файлом по хешу файла
        /// </summary>
        /// <param name="upd">информация о файле на сервере обновлений</param>
        private async void DownloadFileForUpdate(ServerUpdateFile upd)
        {
            var localFilePath = Path.Combine(CommonFunctions.GetAppDirectoryPath, ServerUpdateFile.UpdateFolderPath, upd.FileNameOnServer);
            
            //если файл уже скачан и хеш совпадает - не качаем его и не устанавливаем
            if (File.Exists(localFilePath) &&
                localFilePath.GetHashFile() == upd.Hash) return;

            if (await Task<bool>.Factory
                .StartNew(() => _serverProtocol.DownloadFile(upd.FileNameOnServer, localFilePath)))
            {
                await Task<bool>.Factory.StartNew(upd.Install);

                Log(upd.ErrorMessage);
            }
            else
            {
                Log(_serverProtocol.ErrorMessage);
            }
        }


        private void Log(string message)
        {
            ErrorMessage += message + Environment.NewLine;
        }

        public void Dispose()
        {
            _serverProtocol = null;
            _filesForUpdateInfo = null;
        }
    }
}
