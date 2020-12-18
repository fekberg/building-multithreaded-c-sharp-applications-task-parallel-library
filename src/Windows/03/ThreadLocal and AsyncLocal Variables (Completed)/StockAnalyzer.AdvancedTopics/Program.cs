using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer.AdvancedTopics
{
    class Program
    {
        static AsyncLocal<decimal?> asyncLocal = new AsyncLocal<decimal?>();
        static void Main(string[] args)
        {
            asyncLocal.Value = 200;

            var options = new ParallelOptions { MaxDegreeOfParallelism = 1 };
            Parallel.For(0, 100, options, async (i) => {
                var currentValue = asyncLocal.Value;
                asyncLocal.Value = Compute(i);
            });

            var currentValue = asyncLocal.Value;
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
