using CommunicationAPI;
using CommunicationAPI.Models;
using static CommunicationAPI.Serialization;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Client.DataAPI
{
    public class WebSocketController
    {
        public WebSocketConnection webSocketConnection;
        public Action<string> connectionLogger;
        private IRepository repository;

        string _result = "";

        public event Action<Type> DataUpdate;

        public WebSocketController()
        {
            connectionLogger = message => _result = message;
            repository = Data.GetRepository();
        }

        public async Task<bool> Connect(string peer)
        {

            await WebSocketClient.Connect(new Uri(peer), connectionLogger);
            webSocketConnection = (WebSocketClient.ClientWebSocketConnection)WebSocketClient._socket;
            webSocketConnection.onMessage = message => OnInvokeMessage(message);
            return true;
        }

        public async Task Disconnect()
        {
            await WebSocketClient.Disconnect();
        }

        public async Task<bool> SendTask(string newTask)
        {
            await WebSocketClient.SendTask(newTask);
            return true;
        }

        private void OnInvokeMessage(string message)
        {
            _result = message;
            ParseMessage(_result);
        }

        private void ParseMessage(string message)
        {
            if (message.StartsWith("{\"__type\":\"CClient"))
            {
                CClient client = Deserialize<CClient>(message);
                repository.AddClient(client);
                DataUpdate?.Invoke(typeof(CClient));
            }
            else if (message.StartsWith("[{\"__type\":\"CClient"))
            {
                List<CClient> clients = Deserialize<List<CClient>>(message);
                foreach (CClient c in clients)
                    repository.AddClient(c);
                DataUpdate?.Invoke(typeof(CClient));
            }
            else if (message.StartsWith("{\"__type\":\"COrder"))
            {
                COrder order = Deserialize<COrder>(message);
                repository.AddOrder(order);
                DataUpdate?.Invoke(typeof(COrder));
            }
            else if (message.StartsWith("[{\"__type\":\"COrder"))
            {
                List<COrder> orders = Deserialize<List<COrder>>(message);
                foreach (COrder o in orders)
                    repository.AddOrder(o);
                DataUpdate?.Invoke(typeof(COrder));
            }
            else if (message.StartsWith("{\"__type\":\"CProduct"))
            {
                CProduct product = Deserialize<CProduct>(message);
                repository.AddProduct(product);
                DataUpdate?.Invoke(typeof(CProduct));
            }
            else if (message.StartsWith("[{\"__type\":\"CProduct"))
            {
                List<CProduct> products = Deserialize<List<CProduct>>(message);
                foreach (CProduct p in products)
                    repository.AddProduct(p);
                DataUpdate?.Invoke(typeof(CProduct));
            }
            else if (message.StartsWith("{\"__type\":\"CEvidenceEntry"))
            {
                CEvidenceEntry entry = Deserialize<CEvidenceEntry>(message);
                repository.AddEvidenceEntry(entry);
                DataUpdate?.Invoke(typeof(CEvidenceEntry));
            }
            else if (message.StartsWith("[{\"__type\":\"CEvidenceEntry"))
            {
                List<CEvidenceEntry> evidenceEntries = Deserialize<List<CEvidenceEntry>>(message);
                foreach (CEvidenceEntry e in evidenceEntries)
                    repository.AddEvidenceEntry(e);
                DataUpdate?.Invoke(typeof(CEvidenceEntry));
            }
        }
    }
}
