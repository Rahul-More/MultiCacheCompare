using MutliCacheCompare;

namespace Benchmark
{
    public interface IMyBenchmarkRead
    {
        Task<UserPacked?> Garnet1024();
        Task<UserPacked?> Garnet200();
        Task<UserPacked?> Garnet2048();
        Task<UserPacked?> Garnet4096();
        Task<UserPacked?> Redis1024();
        Task<UserPacked?> Redis200();
        Task<UserPacked?> Redis2048();
        Task<UserPacked?> Redis4096();
    }
}