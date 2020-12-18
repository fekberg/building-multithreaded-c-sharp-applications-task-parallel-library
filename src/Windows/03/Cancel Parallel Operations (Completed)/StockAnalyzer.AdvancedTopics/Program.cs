using System;
using System.Diagnostics;
using System.Threading;
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

            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(2000);

            var parallelOptions = new ParallelOptions
            {
                CancellationToken = cancellationTokenSource.Token,
                MaxDegreeOfParallelism = 1
            };

            int total = 0;
            try
            {
                Parallel.For(0, 100, parallelOptions, (i) =>
                {
                    Interlocked.Add(ref total, (int)Compute(i));
                });
            }
            catch(OperationCanceledException ex)
            {
                Console.WriteLine("Cancellation requested!");
            }
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
