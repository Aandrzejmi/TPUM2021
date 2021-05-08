using SharedData;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Client.DataAPI
{
    public class WebSocketController
    {
        public WebSocketConnection webSocketConnection;
        public Action<string> connectionLogger;

        string _result = "";

        public WebSocketController()
        {
            connectionLogger = message => _result = message;
        }

        public async Task<bool> Connect(string peer)
        {
            await WebSocketClient.Connect(new Uri(peer), connectionLogger);
            webSocketConnection = (WebSocketClient.ClientWebSocketConnection)WebSocketClient._socket;
            webSocketConnection.onMessage = message => OnInvokeMessage(message);
            return true;
        }

        public async Task<bool> SendTask(string newTask)
        {
            await WebSocketClient.SendTask(newTask);
            return true;
        }

        private void OnInvokeMessage(string message)
        {
            _result = message;
            Debug.WriteLine(_result);
        }

        private void ParseMessage(string message)
        {

        }
    }
}
