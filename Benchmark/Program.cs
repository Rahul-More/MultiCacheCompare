namespace Benchmark;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using MutliCacheCompare;

public class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<MyBenchmark>();
    }
    //[SimpleJob(RunStrategy.ColdStart, iterationCount: 100)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class MyBenchmark
    {
        private readonly ICache redis;
        private readonly ICache garnet;
        int randomKey;

        public MyBenchmark()
        {
            redis = new RedisCache();
            garnet = new GarnetRedisClientCache();
            randomKey = new Random(42).Next(0, 2_00_000);
        }

        [Benchmark]
        public async Task<UserPacked?> Garnet200() => await garnet.GetValue(randomKey,200);

        [Benchmark]
        public async Task<UserPacked?> Redis200() => await redis.GetValue(randomKey,200);

        [Benchmark]
        public async Task<UserPacked?> Garnet1024() => await garnet.GetValue(randomKey,1024);

        [Benchmark]
        public async Task<UserPacked?> Redis1024() => await redis.GetValue(randomKey,1024);

        [Benchmark]
        public async Task<UserPacked?> Garnet2048() => await garnet.GetValue(randomKey,2048);

        [Benchmark]
        public async Task<UserPacked?> Redis2048() => await redis.GetValue(randomKey,2048);

        [Benchmark]
        public async Task<UserPacked?> Garnet4096() => await garnet.GetValue(randomKey,4096);

        [Benchmark]
        public async Task<UserPacked?> Redis4096() => await redis.GetValue(randomKey,4096);
        
    }
}
