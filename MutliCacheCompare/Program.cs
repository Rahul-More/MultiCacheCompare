using System;

namespace MutliCacheCompare
{
    internal class Program
    {
        async static Task Main(string[] args)
        {

            Console.WriteLine("Hello, World!");
            ICache redis = new RedisCache();
            ICache garnet = new GarnetRedisClientCache();

            for (int i = 0; i < 1_000_000; i++)
            {
                var user = new UserPacked
                {
                    Id = i,
                    Name = $"UserName_{i}",
                    Unique = Guid.NewGuid()
                };
                Console.WriteLine(user.Name);
                await redis.AddValue(user);
                await garnet.AddValue(user);
            }
        }   
    }
}
