using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace chrismrgn.sdl.tridion.core.FileCache
{
    public static class FileCache
    {
        private static string GetDataFolderPath(string filename)
        {
            var path = Environment.CurrentDirectory + Path.Combine("\\data\\",  filename);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            return path;
        }

        public static T LoadFromFile<T>(string filename) 
        {
            T items = default(T);
            var path = GetDataFolderPath(filename);

            if (Settings.CacheData && File.Exists(path))
            {
                items = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            }

            return items;
        }

        public static void SaveToFile(string filename, object @object)
        {
            var path = GetDataFolderPath(filename);
            if (Settings.CacheData)
                File.WriteAllText(path, JsonConvert.SerializeObject(@object));
        }

    }
}
