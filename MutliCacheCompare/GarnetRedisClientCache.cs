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
            return database.StringSetAsync(BuildKey(data.Id), bytes);
        }

        public string BuildKey(int key) => $"{ICache._key}.{key}";

        public async Task<UserPacked?> GetValue(int id)
        {
            byte[] bytes = await database.StringGetAsync(BuildKey(id));

            if (bytes.Length > 0)
            {
                return MemoryPackSerializer.Deserialize<UserPacked>((byte[])bytes);
            }

            return null;
        }

    }
}
