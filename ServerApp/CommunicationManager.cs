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

        private SessionTimer serverTimer;
        private PingObserver pingObserver;

        private Action<string> GetConnectionLog(Connection con) => (x) => Log($"[Con {con.id}] {x}");

        public CommunicationManager(int webSocketPort, Action<string> log)
        {
            Log = log;

            if (IPEndPoint.MaxPort > webSocketPort && IPEndPoint.MinPort < webSocketPort)
                this.webSocketPort = webSocketPort;
            else
                Log("Wrong port number, using default port");

            InitServerTimer();
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
                con.handler = new MessageHandler(con, GetConnectionLog(con));
                await con.ws.SendAsync(con.handler.Handle(data));
            };
        }

        private void InitSessionTimer(Connection con)
        {
            con.timer = new SessionTimer(60);
            con.observer = new SessionTimeoutObserver(con.ws, GetConnectionLog(con));
            con.observer.Subscribe(con.timer);
            con.timer.Start();
        }
        private void InitServerTimer()
        {
            serverTimer = new SessionTimer(int.MaxValue);
            pingObserver = new PingObserver(this, 15);
            serverTimer.Subscribe(pingObserver);
            serverTimer.Start();
        }

        public void Dispose()
        {
            Log($"Shuting down the communication manager");
            List<Task> _disconnectionTasks = new List<Task>();
            foreach (var con in connections)
                _disconnectionTasks.Add(con.ws.DisconnectAsync());
            Task.WaitAll(_disconnectionTasks.ToArray());
        }


        private class PingObserver : IObserver<SessionTimer.State>
        {
            private CommunicationManager _comManager;
            private readonly int seconds;

            public PingObserver(CommunicationManager comManager, int seconds)
            {
                this._comManager = comManager;
                this.seconds = seconds;
            }

            public void OnCompleted()
            {
                _comManager.InitServerTimer();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(SessionTimer.State value)
            {
                if (value.counter % seconds == 0)
                {
                    _comManager.SendAll("ping");
                    _comManager.Log("Pinging all clients");
                }
            }
        }
    }
}
