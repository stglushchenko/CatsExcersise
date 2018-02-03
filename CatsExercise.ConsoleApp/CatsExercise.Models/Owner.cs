using CatsExercise.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CatsExercise.Models
{
    public class Owner
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("pets")]
        public IEnumerable<Pet> Pets { get; set; }

    }
}
