using BenchmarkDotNet.Attributes;
using MutliCacheCompare;

namespace Benchmark
{
    //[SimpleJob(RunStrategy.ColdStart, iterationCount: 100)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class MyBenchmarkWrite
    {
        static char[] alphabet = "abcdefghijklmnopqrstuvwxyz1324567890".ToCharArray();
        public readonly ICache redis;
        public readonly ICache garnet;
        public readonly ICache scaleout;
        public readonly UserPacked User200;
        public readonly UserPacked User1024;
        public readonly UserPacked User2048;
        public readonly UserPacked User4096;
        int randomKey;

        public MyBenchmarkWrite()
        {
            redis = new RedisCache();
            garnet = new GarnetRedisClientCache();
            scaleout = new ScaleOutCache();

            User200 = new UserPacked
            {
                Id = 100_9200,
                Name = $"UserName_{100_9200}",
                Unique = Guid.NewGuid(),
                Data = Data(200),
                Size = 200,
            };
            User1024 = new UserPacked
            {
                Id = 100_91024,
                Name = $"UserName_{100_9200}",
                Unique = Guid.NewGuid(),
                Data = Data(1024),
                Size = 1024,
            };
            User2048 = new UserPacked
            {
                Id = 100_92048,
                Name = $"UserName_{100_9200}",
                Unique = Guid.NewGuid(),
                Data = Data(2048),
                Size = 2048,
            };
            User4096 = new UserPacked
            {
                Id = 100_94096,
                Name = $"UserName_{100_9200}",
                Unique = Guid.NewGuid(),
                Data = Data(4096),
                Size = 4096,
            };
        }

        public static string Data(int lenght = 15)
        {
            Random rand = new Random();
            return new string(
                Enumerable.Repeat(alphabet, lenght)
                          .Select(s => s[rand.Next(s.Length)])
                          .ToArray()
            );

        }

        [Benchmark]
        public async Task Garnet200() => await garnet.AddValue(User200);

        [Benchmark]
        public async Task Redis200() => await redis.AddValue(User200);

        [Benchmark]
        public async Task Garnet1024() => await garnet.AddValue(User1024);

        [Benchmark]
        public async Task Redis1024() => await redis.AddValue(User1024);

        [Benchmark]
        public async Task Garnet2048() => await garnet.AddValue(User2048);

        [Benchmark]
        public async Task Redis2048() => await redis.AddValue(User2048);

        [Benchmark]
        public async Task Garnet4096() => await garnet.AddValue(User4096);

        [Benchmark]
        public async Task Redis4096() => await redis.AddValue(User4096);
        
        [Benchmark]
        public async Task ScaleOut200() => await scaleout.AddValue(User2048);

        [Benchmark]
        public async Task ScaleOut1024() => await scaleout.AddValue(User4096);

        [Benchmark]
        public async Task ScaleOut2048() => await scaleout.AddValue(User2048);

        [Benchmark]
        public async Task ScaleOut4096() => await scaleout.AddValue(User4096);

    }
}