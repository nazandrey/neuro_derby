using System.IO;
using NeuroDerby.Players;
using Newtonsoft.Json;

namespace NeuroDerby.FileOperations
{
    public class FileSaver
    {
        public static void Save<TSource, TDestination>(string filePath, IConverter<TSource,TDestination> converter, TSource source)
        {
            using (var file = File.CreateText(filePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, converter.Convert(source));
            }
        }
    }
}
