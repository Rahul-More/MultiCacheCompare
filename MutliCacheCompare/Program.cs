using System;

namespace MutliCacheCompare
{
    internal class Program
    {
        static char[] alphabet = "abcdefghijklmnopqrstuvwxyz1324567890".ToCharArray();
        async static Task Main(string[] args)
        {

            Console.WriteLine("Hello, World!");
            ICache redis = new RedisCache();
            ICache garnet = new GarnetRedisClientCache();

            var sizes = new[] { Data(200), Data(1024), Data(2048), Data(4096) };

            for (int i = 0; i < 1_000_000; i++)
            {
                foreach (var size in sizes)
                {
                    var user = new UserPacked
                    {
                        Id = i,
                        Name = $"UserName_{i}",
                        Unique = Guid.NewGuid(),
                        Data = size,
                        Size = size.Length
                    };
                    await redis.AddValue(user);
                    await garnet.AddValue(user);
                    Console.Clear();
                    Console.WriteLine(user.Name);
                }
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
