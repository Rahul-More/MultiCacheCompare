using System;
using System.Timers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace MutliCacheCompare
{
    internal class Program
    {
        bool IsRedis = false;
        bool IsGarnet = false;
        bool IsScaleout = false;
        static char[] alphabet = "abcdefghijklmnopqrstuvwxyz1324567890".ToCharArray();
        async static Task Main(string[] args)
        {
            ICache redis = new RedisCache();
            ICache garnetrc = new GarnetRedisClientCache();
            //ICache garnetgc = new GarnetCache();
            //ICache scaleout = new ScaleOutCache();

            var sizes = new[] { Data(200), Data(1024), Data(2048), Data(4096) };
            var stopwatch = new Stopwatch();
            for (int i = 0; i < 1_00_000; i++)
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
                    //await garnetgc.AddValue(user);
                    await garnetrc.AddValue(user);
                    //await scaleout.AddValue(user);

                }
                Console.Clear();
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
