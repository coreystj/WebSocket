using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSocket.Net.Server
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Send(e.Data);
        }
    }
}
