using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TestApp
{
	[TestFixture]
	public class TimerTests
	{
		[Test]
		public async Task OneTimeTimer()
		{
			var counter = 0;
			var timer = new Timer((_) => counter++, null, 0, 0);
			await Task.Delay(3000);
			counter.Should().Be(1);
		}

		[Test]
		public async Task InfinityTimer()
		{
			var counter = 0;
			Timer timer = null;
			timer = new Timer(async (_) =>
			{
				await Task.Delay(1000);
				counter++;
				timer.Change(0, 0);
			}, null, 0, 0);
			await Task.Delay(3500);
			counter.Should().Be(3);
		}
	}
}
