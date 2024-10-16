using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutliCacheCompare
{
    public class GarnetCache : IGarnetCache
    {
        public async Task AddValue(UserPacked data)
        {
            throw new NotImplementedException();
        }

        public string BuildKey(int key) => $"{ICache._key}.{key}";

        public async Task<UserPacked?> GetValue(int id)
        {
            throw new NotImplementedException();
        }
    }
}
