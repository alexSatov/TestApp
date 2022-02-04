using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TestApp
{
	[TestFixture]
	public class SemaphoreTests
	{
		[Test]
		public void WaitZeroTimeout()
		{
			using var semaphore = new SemaphoreSlim(1, 1);
			var result = semaphore.Wait(0);

			result.Should().BeTrue();
			semaphore.CurrentCount.Should().Be(0);
		}

		[Test]
		public void MultipleRelease()
		{
			using var semaphore = new SemaphoreSlim(1, 1);
			Assert.Throws<SemaphoreFullException>(() => semaphore.Release());
		}

		[Test]
		public void ReleaseAfterDispose()
		{
			var counter = 0;
			var semaphore = new SemaphoreSlim(1, 1);
			semaphore.Wait();

			counter++;

			var task = Task.Run(() =>
			{
				counter++;
				semaphore.Wait();
				counter++;
			});

			Thread.Sleep(100);

			counter.Should().Be(2);

			semaphore.Dispose(); // wait will be deadlocked
			semaphore.Release(); // disposed exception
			task.Wait();

			counter.Should().Be(3);
		}

		[TestCase(1000)]
		[TestCase(1000000)]
		[TestCase(10000000)]
		public void SemaphoreMaxCount(int maxCount)
		{
			GC.Collect();

			var watch = Stopwatch.StartNew();

			var semaphores = Enumerable.Range(0, maxCount)
				.Select(_ => new SemaphoreSlim(1, 1))
				.ToList();

			var size = GC.GetTotalMemory(true) / 1000;

			Console.WriteLine($"{semaphores.Count} of {size}KB created in {watch.Elapsed}");
		}
	}
}
