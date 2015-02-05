using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelfUpdateApp.Protocols;
using SelfUpdateApp.settings;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void XmlSettingsFileTest()
        {
            var sett = new SettingsController(CommonFunctions.GetSettingsFilePath)
            {
                Server=new SmbServer(),
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
        }
    }
}
