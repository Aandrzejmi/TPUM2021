using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommunicationAPI;

namespace Server.App
{
    public class CommunicationManager : IDisposable
    {
        private Action<string> Log { get; }
        private int webSocketPort = 8081;
        private List<WebSocketConnection> Sockets { get; set; } = new List<WebSocketConnection>();
        public CommunicationManager(int webSocketPort, Action<string> log)
        {
            Log = log;
            if (IPEndPoint.MaxPort > webSocketPort && IPEndPoint.MinPort < webSocketPort)
                this.webSocketPort = webSocketPort;
            else
                Log("Wrong port number, using default port");
        }

        public async Task InitServerAsync()
        {
            Log($"Web socket server listening on port {webSocketPort}");
            await WebSocketServer.Server(webSocketPort, async _ws => await InitConnectionAsync(_ws));
        }

        private async Task InitConnectionAsync(WebSocketConnection ws)
        {
            Sockets.Add(ws);
            initMessageHandler(ws);
            initErrorHandler(ws);
            await WriteAsync(ws, "Connected");
        }

        private void initErrorHandler(WebSocketConnection ws)
        {
            ws.onClose = () => closeConnection(ws);
            ws.onError = () => closeConnection(ws);
        }

        private void closeConnection(WebSocketConnection ws)
        {
            Log($"Closing connection to peer: {ws}");
            Sockets.Remove(ws);
        }

        private async Task WriteAsync(WebSocketConnection ws, string message)
        {
            Log($"[Writing message]: {message}");
            await ws.SendAsync(message);
        }

        private async Task SendAll(string message)
        {
            foreach (WebSocketConnection ws in Sockets)
            {
                await ws.SendAsync(message);
            }
        }

        private void initMessageHandler(WebSocketConnection ws)
        {
            ws.onMessage = async (data) =>
            {
                Log($"[Received message]: {data}");


                //Resolve message
                await ws.SendAsync("HEEEEEEEEEEEEELLLOOOOOO THERE");
            };
        }

        public void Dispose()
        {
            Log($"Shuting down the communication manager");
            List<Task> _disconnectionTasks = new List<Task>();
            foreach (WebSocketConnection _item in Sockets)
                _disconnectionTasks.Add(_item.DisconnectAsync());
            Task.WaitAll(_disconnectionTasks.ToArray());
        }
    }
}
