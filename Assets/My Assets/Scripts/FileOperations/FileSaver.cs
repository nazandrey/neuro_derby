using System.IO;
using AutoMapper;
using Newtonsoft.Json;

namespace NeuroDerby.FileOperations
{
    public class FileSaver
    {
        public static void Save<TSource, TDestination>(string filePath, IConfigurationProvider config, TSource source)
        {
            var mapper = config.CreateMapper();
            using (var file = File.CreateText(filePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, mapper.Map<TDestination>(source));
            }
        }
    }
}
