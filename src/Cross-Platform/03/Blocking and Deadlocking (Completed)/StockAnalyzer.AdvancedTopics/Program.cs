using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer.AdvancedTopics
{
    class Program
    {
        static object syncRoot = new object();

        static object lock1 = new object();
        static object lock2 = new object();

        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var t1 = Task.Run(() => { 
                lock(lock1)
                {
                    Thread.Sleep(1);
                    lock(lock2)
                    {
                        Console.WriteLine("Hello!");
                    }    
                }
            });
            var t2 = Task.Run(() => { 
                lock(lock2)
                {
                    Thread.Sleep(1);
                    lock(lock1)
                    {
                        Console.WriteLine("World!");
                    }
                }
            });

            await Task.WhenAll(t1, t2);

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
