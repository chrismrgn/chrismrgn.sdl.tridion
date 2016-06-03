using chrismrgn.sdl.tridion.core.Logging;
using Newtonsoft.Json;
using System;
using System.IO;

namespace chrismrgn.sdl.tridion.core.FileCache
{
    public static class FileCache
    {
        private static string GetDataFolderPath(string subFolder)
        {
            var path = Settings.CacheFolder ?? Environment.CurrentDirectory;
            path = path + "\\data\\" + subFolder;
            Directory.CreateDirectory(path);
            return path;
        }

        public static T LoadFromFile<T>(string filename, string subFolder = "") 
        {
            T items = default(T);
            var path = GetDataFolderPath(subFolder) + "\\" + SanitizeFileName(filename);

            if (Settings.CacheData && File.Exists(path))
            {
                items = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            }

            return items;
        }

        public static void SaveToFile(string filename, object @object, string subFolder = "")
        {
            string path = "";
            if (Settings.CacheData)
            {
                path = GetDataFolderPath(subFolder) + "\\" + SanitizeFileName(filename);
                try
                {
                    File.WriteAllText(path, JsonConvert.SerializeObject(@object));
                }
                catch(PathTooLongException e)
                {
                    Logger.Error("PathTooLongException for {0}", e, path);
                }
            }
        }


        private static string SanitizeFileName(string fileName)
        {
            var sanitizedFileName = fileName;
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                sanitizedFileName = sanitizedFileName.Replace(c.ToString(), "");
            }
            return sanitizedFileName;
        }
    }
}
