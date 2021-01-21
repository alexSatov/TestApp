using System;
using FluentAssertions;
using NUnit.Framework;

namespace TestApp
{
	public class EventTests
	{
		private static event Action TestEvent;
		private static int counter;

		[SetUp]
		public void SetUp()
		{
			counter = 0;
		}

		[Test]
		public void EventUnsubscribe()
		{
			var c1 = new EventContainer();
			TestEvent += OnTestEvent2;
			var c2 = new EventContainer();

			TestEvent -= OnTestEvent2;

			TestEvent?.Invoke();

			counter.Should().Be(2);

			c1.Dispose();
			c1.Dispose();

			TestEvent?.Invoke();

			counter.Should().Be(3);

			c2.Dispose();

			TestEvent?.Invoke();

			counter.Should().Be(3);
		}

		[Test]
		public void MultipleSubscribe()
		{
			TestEvent += OnTestEvent2;
			TestEvent += OnTestEvent2;

			TestEvent?.Invoke();

			counter.Should().Be(10);

			TestEvent -= OnTestEvent2;

			TestEvent?.Invoke();

			counter.Should().Be(15);

			TestEvent -= OnTestEvent2;

			TestEvent?.Invoke();

			counter.Should().Be(15);
		}

		[Test]
		public void SafeUnsubscribe()
		{
			TestEvent -= OnTestEvent2;
		}

		private void OnTestEvent2()
		{
			counter += 5;
		}

		private class EventContainer : IDisposable
		{
			public EventContainer()
			{
				TestEvent += OnTestEvent;
			}

			private void OnTestEvent()
			{
				counter++;
			}

			public void Dispose()
			{
				TestEvent -= OnTestEvent;
			}
		}
	}
}
