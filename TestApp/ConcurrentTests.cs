using System.Collections.Concurrent;
using FluentAssertions;
using NUnit.Framework;

namespace TestApp
{
	[TestFixture]
	public class ConcurrentTests
	{
		[Test]
		public void ConcurrentQueue()
		{
			var queue = new ConcurrentQueue<int>();

			queue.TryDequeue(out var result).Should().BeFalse();

			queue.Enqueue(1);

			queue.TryDequeue(out result).Should().BeTrue();
			result.Should().Be(1);

			queue.TryDequeue(out result).Should().BeFalse();
		}
	}
}
