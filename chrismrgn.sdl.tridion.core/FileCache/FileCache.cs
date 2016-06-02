using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace chrismrgn.sdl.tridion.core.FileCache
{
    public static class FileCache
    {
        private static string GetDataFolderPath(string subFolder)
        {
            var path = Environment.CurrentDirectory + "\\data\\" + subFolder;
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
            var path = GetDataFolderPath(subFolder) + "\\" + SanitizeFileName(filename);
            if (Settings.CacheData)
                File.WriteAllText(path, JsonConvert.SerializeObject(@object));
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
