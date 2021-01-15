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
		public void MultipleRelease()
		{
			var semaphore = new SemaphoreSlim(1, 1);
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
	}
}
