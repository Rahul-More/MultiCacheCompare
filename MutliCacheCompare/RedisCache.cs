using MemoryPack;
using StackExchange.Redis;

namespace MutliCacheCompare
{
    public class RedisCache : IRedisCache
    {
        IDatabase database;
        public RedisCache()
        {
            var con = ConnectionMultiplexer.Connect("localhost:7379");
            database = con.GetDatabase();
        }
        public Task AddValue(UserPacked data)
        {
            byte[] bytes = MemoryPackSerializer.Serialize(data);
            return database.StringSetAsync(BuildKey(data.Id), bytes);
        }

        public string BuildKey(int key) => $"{ICache._key}.{key}";

        public async Task<UserPacked?> GetValue(int id)
        {
            var bytes = await database.StringGetAsync(BuildKey(id));

            if (bytes.HasValue)
            {
                return MemoryPackSerializer.Deserialize<UserPacked>((byte[])bytes);
            }
            return null;
        }
    }
}
