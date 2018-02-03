using CatsExercise.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CatsExercise.Models
{
    public class Pet
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PetType PetType { get; set; }
    }
}
