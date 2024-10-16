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
        public async Task<UserPacked?> Garnet() => await garnet.GetValue(randomKey);

        [Benchmark]
        public async Task<UserPacked?> Redis() => await redis.GetValue(randomKey);
    }
}
