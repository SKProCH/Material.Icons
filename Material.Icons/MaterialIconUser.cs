using Newtonsoft.Json;

namespace Material.Icons {
    public class MaterialIconUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}