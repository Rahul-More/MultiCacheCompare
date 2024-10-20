using System;
using System.Timers;
using System.Diagnostics;
namespace MutliCacheCompare
{
    internal class Program
    {
        static char[] alphabet = "abcdefghijklmnopqrstuvwxyz1324567890".ToCharArray();
        async static Task Main(string[] args)
        {

            Console.WriteLine("Hello, World!");
            ICache redis = new RedisCache();
            ICache garnetrc = new GarnetRedisClientCache();
            ICache garnetgc = new GarnetRedisClientCache();
            ICache scaleout = new ScaleOutCache();

            var sizes = new[] { Data(200), Data(1024), Data(2048), Data(4096) };
            var stopwatch = new Stopwatch();
            for (int i = 0; i < 1_000_000; i++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                var user = new UserPacked
                {
                    Id = i,
                    Name = $"UserName_{i}",
                    Unique = Guid.NewGuid(),
                };
                foreach (var size in sizes)
                {
                    user.Data = size;
                    user.Size = size.Length;
                    await redis.AddValue(user);
                    await garnetgc.AddValue(user);
                    await garnetrc.AddValue(user);
                    await scaleout.AddValue(user);

                }
                // Console.Clear();
                stopwatch.Stop();
                Console.WriteLine($"User {user.Name} inserted into caches in {stopwatch.Elapsed} ");
            }
        }
        static string Data(int lenght = 15)
        {
            Random rand = new Random();
            return new string(
                Enumerable.Repeat(alphabet, lenght)
                          .Select(s => s[rand.Next(s.Length)])
                          .ToArray()
            );

        }
    }
}
