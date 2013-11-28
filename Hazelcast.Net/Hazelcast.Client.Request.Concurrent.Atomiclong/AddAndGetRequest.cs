using Hazelcast.Client.Request.Concurrent.Atomiclong;
using Hazelcast.Serialization.Hook;


namespace Hazelcast.Client.Request.Concurrent.Atomiclong
{
	
	public class AddAndGetRequest : AtomicLongRequest
	{
		public AddAndGetRequest()
		{
		}

		public AddAndGetRequest(string name, long delta) : base(name, delta)
		{
		}

		public override int GetClassId()
		{
			return AtomicLongPortableHook.AddAndGet;
		}
	}
}