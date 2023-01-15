using System.Collections.Generic;
using System.Linq;

namespace Meta {
    public class MaterialIconInfo {
        private string _name;
        private List<string> _aliases;
        
        public string Name
        {
            get => _name;
            set => _name = value?.Underscore().Pascalize();
        }

        public List<string> Aliases
        {
            get => _aliases;
            set => _aliases = value.Select(s => s.Underscore().Pascalize()).ToList();
        }
        
        public string Id { get; set; }
        
        public string Data { get; set; }
        
        public MaterialIconUser User { get; set; }
        
        public long CommentCount { get; set; }
    }
}
