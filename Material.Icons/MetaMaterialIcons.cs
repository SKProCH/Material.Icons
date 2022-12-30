using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Material.Icons {
    internal class MetaMaterialIcons
    {
        [JsonPropertyName("icons")]
        public List<MaterialIconInfo> Icons { get; set; }
    }
}