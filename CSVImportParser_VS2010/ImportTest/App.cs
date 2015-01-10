using System.IO;
using System.Reflection;

namespace ImportTest
{
    public static class App
    {

        public static string GetAppPath
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";
            }
        }

    }
}
