using BenchmarkDotNet.Attributes;
using MutliCacheCompare;

namespace Benchmark
{  
    //[SimpleJob(RunStrategy.ColdStart, iterationCount: 100)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class MyBenchmarkRead
    {
        private readonly ICache redis;
        private readonly ICache garnet;
        private readonly ICache scaleout;
        int randomKey;

        public MyBenchmarkRead()
        {
            redis = new RedisCache();
            garnet = new GarnetRedisClientCache();
            scaleout = new ScaleOutCache();
            randomKey = new Random().Next(0, 1_00_000);
        }

        [Benchmark]
        public async Task<UserPacked?> Garnet200() => await garnet.GetValue(randomKey, 200);

        [Benchmark]
        public async Task<UserPacked?> Redis200() => await redis.GetValue(randomKey, 200);

        [Benchmark]
        public async Task<UserPacked?> Garnet1024() => await garnet.GetValue(randomKey, 1024);

        [Benchmark]
        public async Task<UserPacked?> Redis1024() => await redis.GetValue(randomKey, 1024);

        [Benchmark]
        public async Task<UserPacked?> Garnet2048() => await garnet.GetValue(randomKey, 2048);

        [Benchmark]
        public async Task<UserPacked?> Redis2048() => await redis.GetValue(randomKey, 2048);

        [Benchmark]
        public async Task<UserPacked?> Garnet4096() => await garnet.GetValue(randomKey, 4096);

        [Benchmark]
        public async Task<UserPacked?> Redis4096() => await redis.GetValue(randomKey, 4096);
        
        [Benchmark]
        public async Task<UserPacked?> ScaleOut200() => await scaleout.GetValue(randomKey, 200);

        [Benchmark]
        public async Task<UserPacked?> ScaleOut1024() => await scaleout.GetValue(randomKey, 1024);

        [Benchmark]
        public async Task<UserPacked?> ScaleOut2048() => await scaleout.GetValue(randomKey, 2048);

        [Benchmark]
        public async Task<UserPacked?> ScaleOut4096() => await scaleout.GetValue(randomKey, 4096);

    }
}