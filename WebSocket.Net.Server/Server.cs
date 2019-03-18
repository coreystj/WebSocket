using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSocket.Net.Server
{
    public static class Server
    {
        private static WebSocketServer _server;
        public static void Main(string[] args)
        {
            _server = new WebSocketServer("ws://192.168.0.182:4649");
            _server.AddWebSocketService<Echo>("/Echo");
            _server.Start();
            if (_server.IsListening)
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", _server.Port);
                foreach (var path in _server.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }

            Console.WriteLine("\nPress Enter key to stop the server...");
            Console.ReadLine();

            _server.Stop();
        }
    }
}
