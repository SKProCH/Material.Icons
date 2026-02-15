using IconGenerators.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IconGenerators
{
    public interface IIconPackGenerator
    {
        public string Name { get; }
        public Task<IEnumerable<IconInfo>> Fetch();
    }
}
