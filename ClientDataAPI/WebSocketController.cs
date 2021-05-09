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
            var split = message.Split("#");
            switch (split[0])
            {
                case "clientL":
                    List<CClient> clients = Deserialize<List<CClient>>(split[1]);
                    foreach (CClient c in clients)
                        repository.AddClient(c);
                    break;

                case "orderL":
                    List<COrder> orders = Deserialize<List<COrder>>(split[1]);
                    foreach (COrder o in orders)
                        repository.AddOrder(o);
                    break;

                case "productL":
                    List<CProduct> products = Deserialize<List<CProduct>>(split[1]);
                    foreach (CProduct p in products)
                        repository.AddProduct(p);
                    break;

                case "entryL":
                    List<CEvidenceEntry> evidenceEntries = Deserialize<List<CEvidenceEntry>>(split[1]);
                    foreach (CEvidenceEntry e in evidenceEntries)
                        repository.AddEvidenceEntry(e);
                    break;

                case "client":
                    CClient client = Deserialize<CClient>(split[1]);
                    repository.AddClient(client);
                    break;

                case "order":
                    COrder order = Deserialize<COrder>(split[1]);
                    repository.AddOrder(order);
                    break;

                case "product":
                    CProduct product = Deserialize<CProduct>(split[1]);
                    repository.AddProduct(product);
                    break;

                case "entry":
                    CEvidenceEntry entry = Deserialize<CEvidenceEntry>(split[1]);
                    repository.AddEvidenceEntry(entry);
                    break;
            }
        }
    }
}
