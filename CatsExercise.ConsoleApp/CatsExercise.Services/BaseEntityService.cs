using CatsExercise.Interfaces.Services;
using CatsExercise.Services.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public BaseEntityService(IConfiguration configuration, ILogger logger)
        {
            _baseServicePath = configuration.GetSection("BaseServicePath").Value;
            _entityPath = configuration.GetSection("EntitiesPaths")[typeof(T).Name];
            _logger = logger;
        }

        public async Task<IEnumerable<T>> All()
        {
            try
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
            catch (Exception ex) when 
            (
                ex is HttpRequestException
                || ex is ArgumentNullException
                || ex is JsonSerializationException
            )
            {
                var errorMessage = $"Error occured while retrieving All entities of type {typeof(T).Name}\r\nErrorMessage: {ex.Message}";

                _logger.LogError(ex, errorMessage);

                throw new ServiceLayerException(errorMessage);
            }
        }
    }
}
