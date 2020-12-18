using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StockAnalyzer.AdvancedTopics
{
    class Program
    {
        static object syncRoot = new object();
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            decimal total = 0;

            Parallel.For(0, 100, (i) => {
                var result = Compute(i);
                lock (syncRoot) // Only lock for a short time!
                {
                    total += result;
                }
            });

            Console.WriteLine(total);

            Console.WriteLine($"It took: {stopwatch.ElapsedMilliseconds}ms to run");
        }

        static Random random = new Random();
        static decimal Compute(int value)
        {
            var randomMilliseconds = random.Next(10, 50);
            var end = DateTime.Now + TimeSpan.FromMilliseconds(randomMilliseconds);

            // This will spin for a while...
            while (DateTime.Now < end) { }

            return value + 0.5m;
        }
    }
}
