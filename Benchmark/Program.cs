namespace Benchmark;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using MutliCacheCompare;

public class Program
{
    static void Main(string[] args)
    {
        //var summaryRead = BenchmarkRunner.Run<MyBenchmarkRead>();
        var summaryWrite = BenchmarkRunner.Run<MyBenchmarkWrite>();
    }
}
