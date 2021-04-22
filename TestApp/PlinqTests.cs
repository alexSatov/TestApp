using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TestApp
{
    [TestFixture]
    public class PlinqTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void QueryDowncast2(bool parallel)
        {
            var query = parallel
                ? Enumerable.Range(0, 10).AsParallel().Select(i => $"{Thread.CurrentThread.ManagedThreadId}: {i}")
                : Enumerable.Range(0, 10).Select(i => $"{Thread.CurrentThread.ManagedThreadId}: {i}");

            Console.WriteLine(string.Join("\r\n", query));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void QueryDowncast(bool parallel)
        {
            var query = parallel
                ? Enumerable.Range(0, 100000000).AsParallel()
                : Enumerable.Range(0, 100000000);

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
        public void ParallelTasks()
        {
            var watch = Stopwatch.StartNew();

            var tasks = Enumerable.Range(0, 10)
                .Select(t => Task.Run(() => Enumerable.Range(t * 10000000, 10000000)
                    .Select(i => Math.Cos(i) + Math.Sin(i))
                    .ToList()));

            var result = tasks.SelectMany(t => t.Result).ToList();

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

        [Test]
        public void ListAdd_NotThreadSafe()
        {
            var target = new List<int>();

            var source = ParallelEnumerable.Range(0, 100000000)
                .Select(i =>
                {
                    target.Add(i);
                    return i;
                })
                .ToList();

            target.Count.Should().NotBe(source.Count);
        }
    }
}
