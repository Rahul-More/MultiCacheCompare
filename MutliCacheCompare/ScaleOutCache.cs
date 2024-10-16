using MemoryPack;
using Scaleout.Client;
using System.Text;

namespace MutliCacheCompare
{
    public class ScaleOutCache : ICache
    {
        const string _connectionString = "";
        Cache<string, UserPacked> cache;
        public ScaleOutCache()
        {
            var gridConnection = GridConnection.Connect(_connectionString);
            var cachebuilder = new CacheBuilder<string, UserPacked>(ICache._key, gridConnection)
                .SetSerialization(Serialize, Deserialize);

        }
        public Task AddValue(UserPacked data)
        {
            var key = BuildKey(data.Id);
            return cache.AddAsync(key, data);
        }

        public async Task<UserPacked?> GetValue(int id)
        {
            var key = BuildKey(id);
            var response = await cache.ReadAsync(key);
            return response.Result == ServerResult.Retrieved ? response.Value : null;
        }

        public static UserPacked Deserialize(Stream stream)
        {
            byte[] data;
            using var binaryReader = new BinaryReader(stream, Encoding.UTF8, true);
            data = binaryReader.ReadBytes((int)stream.Length);
            return MemoryPackSerializer.Deserialize<UserPacked>(data)!;
        }

        public static void Serialize(UserPacked data, Stream stream)
        {
            var bytes = MemoryPackSerializer.Serialize(data);
            using var binaryWriter = new BinaryWriter(stream, Encoding.UTF8, true);
            binaryWriter.Write(bytes, 0, bytes.Length);
        }

        public string BuildKey(int key) => $"{ICache._key}.{key}";
    }
}
