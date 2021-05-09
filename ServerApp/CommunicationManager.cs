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
        private List<Connection> connections = new List<Connection>();

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
            var con = new Connection();

            connections.Add(con);

            con.ws = ws;
            InitMessageHandler(con);
            InitErrorHandler(con);
            InitSessionTimer(con);
            await WriteAsync(con.ws, "Connected");
        }

        private void InitErrorHandler(Connection con)
        {
            con.ws.onClose = () => CloseConnection(con);
            con.ws.onError = () => CloseConnection(con);
        }

        private void CloseConnection(Connection con)
        {
            Log($"Closing connection to peer: {con.ws}");
            connections.Remove(con);
        }

        private async Task WriteAsync(WebSocketConnection ws, string message)
        {
            Log($"[Writing message]: {message}");
            await ws.SendAsync(message);
        }

        private async Task SendAll(string message)
        {
            foreach (var con in connections)
            {
                await con.ws.SendAsync(message);
            }
        }

        private void InitMessageHandler(Connection con)
        {
            con.ws.onMessage = async (data) =>
            {
                con.handler = new MessageHandler(con, Log);
                await con.ws.SendAsync(con.handler.Handle(data));
            };
        }

        private void InitSessionTimer(Connection con)
        {
            con.timer = new SessionTimer(60);
            con.observer = new SessionTimeoutObserver(con.ws, Log);
            con.observer.Subscribe(con.timer);
            con.timer.Start();
        }

        public void Dispose()
        {
            Log($"Shuting down the communication manager");
            List<Task> _disconnectionTasks = new List<Task>();
            foreach (var con in connections)
                _disconnectionTasks.Add(con.ws.DisconnectAsync());
            Task.WaitAll(_disconnectionTasks.ToArray());
        }
    }
}
