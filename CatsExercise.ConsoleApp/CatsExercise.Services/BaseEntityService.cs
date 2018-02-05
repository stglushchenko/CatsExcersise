using CatsExercise.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatsExercise.Services
{
    public abstract class BaseEntityService<T> : IEntityService<T> where T: class
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private readonly string _baseServicePath;

        private readonly string _entityPath;
        
        public BaseEntityService(IConfiguration configuration)
        {
            _baseServicePath = configuration.GetSection("BaseServicePath").Value;
            _entityPath = configuration.GetSection("EntitiesPaths")[typeof(T).Name];
        }

        public async Task<IEnumerable<T>> All()
        {
            using (Stream stream = await _httpClient.GetStreamAsync($"{_baseServicePath}{_entityPath}"))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    using (JsonReader jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();

                        return serializer.Deserialize<IEnumerable<T>>(jsonReader);
                    }
                }
            }
        }
    }
}
