using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NUnit.Framework;

namespace TestApp
{
	[TestFixture]
	public class DataflowTests
	{
		[Test]
		public void ActionBlock()
		{
			var options = new ExecutionDataflowBlockOptions
			{
				MaxMessagesPerTask = 1
			};

			var blockA = new ActionBlock<int>(i => Console.WriteLine($"A {i}"), options);
			var blockB = new ActionBlock<int>(i => Console.WriteLine($"B {i}"), options);

			for (int i = 0; i < 5; i++)
			{
				blockA.Post(i);
				blockB.Post(i);
			}

			blockA.Complete();
			blockB.Complete();

			Task.WaitAll(blockA.Completion, blockB.Completion);
		}

		[Test]
		public async Task ActionBlock_Async()
		{
			var options = new ExecutionDataflowBlockOptions
			{
				MaxMessagesPerTask = 1
			};

			var blockA = new ActionBlock<int>(async i =>
			{
				await Task.Delay(10);
				Console.WriteLine($"A {i}");
			}, options);

			var blockB = new ActionBlock<int>(async i =>
			{
				await Task.Delay(10);
				Console.WriteLine($"B {i}");
			}, options);

			for (int i = 0; i < 5; i++)
			{
				blockA.Post(i);
				blockB.Post(i);
			}

			blockA.Complete();
			blockB.Complete();

			await Task.WhenAll(blockA.Completion, blockB.Completion);
		}

		[Test]
		public void AutoResetEvent()
		{
			var e = new AutoResetEvent(false);
			e.Set();
			e.WaitOne();
		}
	}
}
