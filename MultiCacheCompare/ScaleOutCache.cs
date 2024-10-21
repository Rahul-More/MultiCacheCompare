using MemoryPack;
using Scaleout.Client;
using System.Text;

namespace MutliCacheCompare
{
    public class ScaleOutCache : ICache
    {
        const string _connectionString = "bootstrapGateways=10.1.0.5:4721";
        Cache<string, UserPacked> cache;
        public ScaleOutCache()
        {
            var gridConnection = GridConnection.Connect(_connectionString);
            var cachebuilder = new CacheBuilder<string, UserPacked>(ICache._key, gridConnection)
                .SetSerialization(Serialize, Deserialize);
            cache = cachebuilder.Build();
        }
        
        public Task AddValue(UserPacked data)
        {
            var key = BuildKey(data.Id, data.Size);
            return cache.AddAsync(key, data);
        }

        public async Task<UserPacked?> GetValue(int id, int size)
        {
            var key = BuildKey(id, size);
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

        public string BuildKey(int key, int size) => $"{ICache._key}.{size}.{key}";
    }
}
