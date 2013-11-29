using Hazelcast.Core;

namespace Hazelcast.Client
{
    internal sealed class HazelcastClientManagedContext : IManagedContext
    {
        private readonly IManagedContext externalContext;

        private readonly bool hasExternalContext;
        private readonly IHazelcastInstance instance;

        public HazelcastClientManagedContext(IHazelcastInstance instance, IManagedContext externalContext)
        {
            this.instance = instance;
            this.externalContext = externalContext;
            hasExternalContext = this.externalContext != null;
        }

        public object Initialize(object obj)
        {
            if (obj is IHazelcastInstanceAware)
            {
                ((IHazelcastInstanceAware) obj).SetHazelcastInstance(instance);
            }
            if (hasExternalContext)
            {
                obj = externalContext.Initialize(obj);
            }
            return obj;
        }
    }
}