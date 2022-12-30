using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Material.Icons {
    public class MaterialIconInfo {
        public string Name { get; set; }

        public List<string> Aliases { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        private string SourceName {
            set => Name = value?.Underscore().Pascalize();
        }

        [JsonPropertyName("aliases")]
        private List<string> SourceAliases {
            set => Aliases = value.Select(s => s.Underscore().Pascalize()).ToList();
        }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("user")]
        public MaterialIconUser User { get; set; }

        [JsonPropertyName("commentCount")]
        public long CommentCount { get; set; }
    }
}