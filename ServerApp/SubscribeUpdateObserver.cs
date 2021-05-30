using CommunicationAPI;
using CommunicationAPI.Models;
using static CommunicationAPI.Serialization;
using Server.LogicAPI;
using Server.LogicAPI.Interfaces;
using System;
using System.Threading.Tasks;

namespace Server.App
{
    class SubscribeUpdateObserver : IObserver<SessionTimer.State>
    {
        private readonly IClientService _clientService = Logic.CreateClientService();
        private readonly IProductService _productService = Logic.CreateProductService();
        private readonly IEvidenceEntryService _evidenceEntryService = Logic.CreateEvidenceEntryService();
        private readonly IOrderService _orderService = Logic.CreateOrderService();
        private readonly Func<string, Task> SendMessage;
        private readonly Action<string> Log;
        private int interval;

        public SubscribeUpdateObserver(Func<string, Task> sendMessage, Action<string> log, int interval)
        {
            SendMessage = sendMessage;
            Log = log;
            this.interval = interval;
        }

        public void OnCompleted() {}

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(SessionTimer.State value)
        {
            if (value.counter % interval == 0)
            {
                var tasks = new Task[4];

                var clients = _clientService.GetAllClients();
                string msgC = Serialize(clients);
                tasks[0] = SendMessage(msgC);

                var products = _productService.GetAllProducts();
                string msgP = Serialize(products);
                tasks[1] = SendMessage(msgP);

                var evEntries = _evidenceEntryService.GetAllEvidenceEntries();
                string msgE = Serialize(evEntries);
                tasks[2] = SendMessage(msgE);

                var orders = _orderService.GetAllOrders();
                string msgO = Serialize(orders);
                tasks[3] = SendMessage(msgO);

                Task.WaitAll(tasks);
                Log("Sending update to subscriber");
            }
        }
    }
}
