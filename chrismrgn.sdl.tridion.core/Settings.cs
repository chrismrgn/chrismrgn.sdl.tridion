using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
