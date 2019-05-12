using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RickAndMorty.Extensions
{
    public static class StreamExtensions
    {
        public static T Deserialize<T>(this Stream stream) where T : class
        {
            if (stream == null)
                return default(T);

            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return new JsonSerializer().Deserialize<T>(jsonReader);
            }
        }
    }
}
