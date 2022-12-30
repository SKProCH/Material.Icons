using System.Text.Json.Serialization;

namespace Material.Icons {
    public class MaterialIconUser
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}