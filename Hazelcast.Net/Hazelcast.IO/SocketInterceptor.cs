using System.Collections.Generic;
using System.Net.Sockets;

namespace Hazelcast.IO
{
    public interface SocketInterceptor
    {
        void Init(Dictionary<string, string> properties);

        /// <exception cref="System.IO.IOException"></exception>
        void OnConnect(Socket connectedSocket);
    }
}