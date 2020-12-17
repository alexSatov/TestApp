using org.apache.zookeeper;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp
{
	class ZooKeeperTest
	{
		public static async Task RunAsync()
		{
			var newClient = new ZooKeeper("localhost:2181", 10000, new EmptyWatcher());
			var state = newClient.getState();
			Thread.Sleep(1000);
			state = newClient.getState();
			await newClient.createAsync("/test", new byte[0], ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL).ConfigureAwait(false);
			state = newClient.getState();
			await Task.Delay(100);
		}
	}

	public class EmptyWatcher : Watcher
	{
		public override Task process(WatchedEvent @event)
		{
			return Task.CompletedTask;
		}
	}
}
