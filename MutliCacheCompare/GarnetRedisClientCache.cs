using MemoryPack;
using StackExchange.Redis;


namespace MutliCacheCompare
{
    public class GarnetRedisClientCache : IGarnetCache
    {
        IDatabase database;

        public GarnetRedisClientCache()
        {
            var con = ConnectionMultiplexer.Connect("localhost:6379");
            database = con.GetDatabase();
        }
        public Task AddValue(UserPacked data)
        {
            byte[] bytes = MemoryPackSerializer.Serialize(data);
            return database.StringSetAsync(BuildKey(data.Id, data.Size), bytes);
        }

        public string BuildKey(int key, int size) => $"{ICache._key}.rc.{size}.{key}";

        public async Task<UserPacked?> GetValue(int id, int size)
        {
            byte[]? bytes = await database.StringGetAsync(BuildKey(id, size));

            if (bytes is not null)
            {
                return MemoryPackSerializer.Deserialize<UserPacked>(bytes);
            }

            return null;
        }

    }
}
