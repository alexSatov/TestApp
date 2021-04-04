using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace TestApp
{
    [TestFixture]
    public class PlinqTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void QueryDowncast(bool parallel)
        {
            var query = parallel
                ? Enumerable.Range(0, 100000000)
                : Enumerable.Range(0, 100000000).AsParallel();

            var watch = Stopwatch.StartNew();
            var result = query.Select(i => Math.Cos(i) + Math.Sin(i)).ToList();
            watch.Stop();

            Console.WriteLine($"Result count {result.Count}, parallel {parallel}: {watch.Elapsed}");
        }

        [Test]
        public void Parallel()
        {
            var watch = Stopwatch.StartNew();

            var result = Enumerable.Range(0, 100000000)
                .AsParallel()
                .Select(i => Math.Cos(i) + Math.Sin(i))
                .ToList();

            watch.Stop();

            Console.WriteLine($"Result count {result.Count}, parallel true: {watch.Elapsed}");
        }

        [Test]
        public void Sync()
        {
            var watch = Stopwatch.StartNew();

            var result = Enumerable.Range(0, 100000000)
                .Select(i => Math.Cos(i) + Math.Sin(i))
                .ToList();

            watch.Stop();

            Console.WriteLine($"Result count {result.Count}, parallel false: {watch.Elapsed}");
        }
    }
}
