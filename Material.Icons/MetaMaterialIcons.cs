﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Material.Icons {
    internal class MetaMaterialIcons
    {
        [JsonProperty("icons")]
        public List<MaterialIconInfo> Icons { get; set; }
    }
}