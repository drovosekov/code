using System;
using SelfUpdateApp.settings.properties;

namespace SelfUpdateApp.Protocols
{
    public interface IGetFile
    {
        PropertyDescriptionString ServerName { get; set; }

        PropertyDescriptionString ServerFileAdress { get; set; }

        PropertyDescriptionString ServerLogin { get; set; }

        PropertyDescriptionString ServerPassword { get; set; }

        bool DownloadFileTo(string destinationFullFileName);

        DateTime FileOnServerCreationDateTime { get; }

        //string ErrorMessage { get; set; }
    }
}