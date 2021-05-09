using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CommunicationAPI;

namespace Server.App
{
    public class CommunicationManager : IDisposable
    {
        private Action<string> Log { get; }
        private int webSocketPort = 8081;
        private List<WebSocketConnection> sockets = new List<WebSocketConnection>();
        private List<MessageHandler> handlers = new List<MessageHandler>();
        private List<SessionTimer> timers = new List<SessionTimer>();

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
            sockets.Add(ws);
            InitMessageHandler(ws);
            InitErrorHandler(ws);
            InitSessionTimer(ws);
            await WriteAsync(ws, "Connected");
        }

        private void InitErrorHandler(WebSocketConnection ws)
        {
            ws.onClose = () => CloseConnection(ws);
            ws.onError = () => CloseConnection(ws);
        }

        private void CloseConnection(WebSocketConnection ws)
        {
            Log($"Closing connection to peer: {ws}");
            sockets.Remove(ws);
        }

        private async Task WriteAsync(WebSocketConnection ws, string message)
        {
            Log($"[Writing message]: {message}");
            await ws.SendAsync(message);
        }

        private async Task SendAll(string message)
        {
            foreach (WebSocketConnection ws in sockets)
            {
                await ws.SendAsync(message);
            }
        }

        private void InitMessageHandler(WebSocketConnection ws)
        {
            var handler = new MessageHandler(ws, Log);
            handlers.Add(handler);
            ws.onMessage = handler.Handle;
        }

        private void InitSessionTimer(WebSocketConnection ws)
        {
            var timer = new SessionTimer(5);
            timer.OnTimeout += () => ws.DisconnectAsync();
            timers.Add(timer);
            timer.Start();
        }

        public void Dispose()
        {
            Log($"Shuting down the communication manager");
            List<Task> _disconnectionTasks = new List<Task>();
            foreach (WebSocketConnection _item in sockets)
                _disconnectionTasks.Add(_item.DisconnectAsync());
            Task.WaitAll(_disconnectionTasks.ToArray());
        }
    }
}
