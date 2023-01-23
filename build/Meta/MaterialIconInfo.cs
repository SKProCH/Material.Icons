using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Meta {
    public class MaterialIconInfo {
        public string Name { get; set; }

        public List<MaterialIconInfoAlias> Aliases { get; set; }

        public string Id { get; set; }

        public string Data { get; set; }

        public MaterialIconUser User { get; set; }

        public long CommentCount { get; set; }
    }
}
