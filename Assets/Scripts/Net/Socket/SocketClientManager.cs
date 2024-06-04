using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Socket 
{
    public class SocketClientManager : IDisposable
    {
        private WebSocketClient socketClient;

        public void CreateSocketClientManager()
        {
            if (socketClient != null && socketClient.host.Equals(NetUrl.Instance.host))
                return;

        }

        public void Dispose()
        {
        }
    }

}
