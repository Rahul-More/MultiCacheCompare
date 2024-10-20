
namespace MutliCacheCompare
{
    public interface IGarnetCache : ICache;
    public interface IScaleOutCache : ICache;
    public interface IRedisCache : ICache;

    public interface ICache
    {
        public const string _key = "User_cache";
        public Task AddValue(UserPacked data);
        public Task<UserPacked?> GetValue(int key,int size);
        string BuildKey(int key, int size);
    }
}