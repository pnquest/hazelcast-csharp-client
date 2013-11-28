using Hazelcast.Client.Request.Base;
using Hazelcast.Client.Request.Multimap;
using Hazelcast.Serialization.Hook;


namespace Hazelcast.Client.Request.Multimap
{
	
	public class EntrySetRequest : MultiMapAllPartitionRequest, IRetryableRequest
	{
		public EntrySetRequest()
		{
		}

		public EntrySetRequest(string name) : base(name)
		{
		}

		public override int GetClassId()
		{
			return MultiMapPortableHook.EntrySet;
		}
	}
}