using System.Configuration;

namespace chrismrgn.sdl.tridion.core
{
    public static class Settings
    {
        public static int MaxThreads
        {
            get
            {
                int maxThreads;
                if (!int.TryParse(ConfigurationManager.AppSettings["maxThreads"], out maxThreads))
                    maxThreads = 1;
                return maxThreads;
            }
        }

        public static bool CacheData
        {
            get
            {
                bool cacheData;
                if (!bool.TryParse(ConfigurationManager.AppSettings["cacheData"], out cacheData))
                    cacheData = false;
                return cacheData;
            }
        }

        public static string CacheFolder
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("CacheFolder");
            }
        }
    }
}
