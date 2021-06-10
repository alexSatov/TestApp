using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TestApp
{
	[TestFixture]
	public class ActionTests
	{
		[Test]
		public void TestAsyncAction()
		{
			var resource = 0;
			Action action = async () =>
			{
				resource = 1;
				await Task.Delay(5000);
				resource = 2;
			};

			action.Invoke();

			resource.Should().Be(1);
		}
	}
}
