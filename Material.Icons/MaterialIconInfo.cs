using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Material.Icons {
    public class MaterialIconInfo {
        public string Name { get; set; }

        public List<string> Aliases { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        private string SourceName {
            set => Name = value?.Underscore().Pascalize();
        }

        [JsonProperty("aliases")]
        private List<string> SourceAliases {
            set => Aliases = value.Select(s => s.Underscore().Pascalize()).ToList();
        }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("user")]
        public MaterialIconUser User { get; set; }

        [JsonProperty("commentCount")]
        public long CommentCount { get; set; }
    }
}