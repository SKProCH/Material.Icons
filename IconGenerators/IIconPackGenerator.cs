using IconGenerators.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IconGenerators
{
    public interface IIconPackGenerator
    {
        public string Name { get; }
        public Task<IEnumerable<IconInfo>> FetchIconData();
    }
}
