﻿using System.IO;
using Newtonsoft.Json;

namespace NeuroDerby.FileOperations
{
    public class FileLoader
    {
        public static TResult Load<TResult>(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    var json = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<TResult>(json);
                }
            }
            return default;
        }
    }
}
