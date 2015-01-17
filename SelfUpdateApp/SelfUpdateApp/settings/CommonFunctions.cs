using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace SelfUpdateApp.settings
{
    public static class CommonFunctions
    {
        internal static string GetAppFullPath
        {
            get
            {
                return Assembly.GetExecutingAssembly().Location;
            }
        }
        internal static string GetAppUpdatePath
        {
            get
            {
                return String.Format(@"{0}\update\", GetAppDirectoryPath);
            }
        }
        internal static string GetSettingsFilePath
        {
            get
            {
                return String.Format(@"{0}\settings.xml", GetAppDirectoryPath);
            }
        }
        internal static string GetAppDirectoryPath
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        internal static string GetHashFile(string filePath)
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                HashAlgorithm ahash = HashAlgorithm.Create();
                var hash = String.Empty;
                hash = ahash.ComputeHash(fs).Aggregate(hash, (current, b) => current + b.ToString("x2").ToLower());

                return hash;
            }
        }

        internal static string GetHashOfStream(this FileStream fs)
        {
            using (var stream = new BufferedStream(fs, 1000000))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
            } 
        }
    }
}
