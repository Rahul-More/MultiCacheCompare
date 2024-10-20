using Garnet.client;
using MemoryPack;

namespace MutliCacheCompare
{
    public class GarnetCache : IGarnetCache
    {
        private readonly GarnetClient client;
        public GarnetCache()
        {
            client = new GarnetClient("127.0.0.1", 6379);
        }
        public async Task AddValue(UserPacked data)
        {
            await client.ConnectAsync();
            var key = MemoryPackSerializer.Serialize(BuildKey(data.Id, data.Size)).AsMemory();
            var bytes = MemoryPackSerializer.Serialize(data).AsMemory();
            await client.StringSetAsync(key, bytes, default);
        }

        public string BuildKey(int key, int size) => $"{ICache._key}.gc.{size}.{key}";

        public async Task<UserPacked?> GetValue(int key, int size)
        {
            await client.ConnectAsync();
            var dbkey = MemoryPackSerializer.Serialize(BuildKey(key, size)).AsMemory();
            var dbvalue = await client.StringGetAsMemoryAsync(dbkey, default);
            return MemoryPackSerializer.Deserialize<UserPacked>(dbvalue.Memory.ToArray());
        }
    }
}
