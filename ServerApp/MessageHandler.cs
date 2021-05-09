using CommunicationAPI;
using CommunicationAPI.Models;
using Server.LogicAPI;
using Server.LogicAPI.Interfaces;
using System;
using System.Threading.Tasks;

namespace Server.App
{
    class MessageHandler
    {
        private readonly IClientService _clientService = Logic.CreateClientService();
        private readonly IProductService _productService = Logic.CreateProductService();
        private readonly IEvidenceEntryService _evidenceEntryService = Logic.CreateEvidenceEntryService();
        private readonly IOrderService _orderService = Logic.CreateOrderService();
        private readonly Connection _con;
        private readonly Action<string> Log;
        private uint counter = 0;

        public MessageHandler(Connection con, Action<string> logFinction)
        {
            _con = con;
            Log = logFinction;
        }

        public void Handle(string data)
        {
            Task.Run(() =>
            {
                _con.timer.Reset();

                uint no = counter++;
                Log($"[Received message {no}]: {data}");

                var split = data.Split("#");

                switch (split[0])
                {
                    case "send":
                        // send [type] [id]
                        // send [type] all
                        HandleSend(split, no);
                        break;

                    case "add":
                        // add [type] [json]
                        HandleAdd(split, no);
                        break;

                    case "update":
                        // update [type] [id] [json]
                        HandleUpdate(split, no);
                        break;

                    default:
                        HandleError(no);
                        break;
                }
            });
        }

        private void HandleSend(string[] split, uint no)
        {
            int id;
            bool all;
            
            if (!ParseId(split[2], out id, out all))
            {
                HandleError(no);
                return;
            }

            if (all)
                RespondSendAll(split, no);
            else
                RespondSend(split, no, id);

        }

        private void HandleAdd(string[] split, uint no)
        {
            try
            {
                switch (split[1])
                {
                    case "client":
                        _clientService.AddClient(Serialization.Deserialize<CClient>(split[2]));
                        Log($"[{no} - Add request]: client added");
                        break;

                    case "product":
                        _productService.AddProduct(Serialization.Deserialize<CProduct>(split[2]));
                        Log($"[{no} - Add request]: product added");
                        break;

                    case "entry":
                        _evidenceEntryService.AddEvidenceEntry(Serialization.Deserialize<CEvidenceEntry>(split[2]));
                        Log($"[{no} - Add request]: evidence entry added");
                        break;

                    case "order":
                        _orderService.AddOrder(Serialization.Deserialize<COrder>(split[2]));
                        Log($"[{no} - Add request]: order added");
                        break;
                }
            }
            catch
            {
                Log($"[{no} - Add request]: exception caugth.");
            }
        }

        private void HandleUpdate(string[] split, uint no)
        {
            try
            {
                int id = int.Parse(split[2]);

                switch (split[1])
                {
                    case "client":
                        _clientService.ChangeClient(id, Serialization.Deserialize<CClient>(split[3]));
                        Log($"[{no} - Update request]: client updated");
                        break;

                    case "product":
                        _productService.ChangeProduct(id, Serialization.Deserialize<CProduct>(split[3]));
                        Log($"[{no} - Update request]: product updated");
                        break;

                    case "entry":
                        _evidenceEntryService.ChangeEvidenceEntry(id, Serialization.Deserialize<CEvidenceEntry>(split[3]));
                        Log($"[{no} - Update request]: evidence entry updated");
                        break;

                    case "order":
                        _orderService.ChangeOrder(id, Serialization.Deserialize<COrder>(split[3]));
                        Log($"[{no} - Update request]: order updated");
                        break;
                }
            }
            catch
            {
                Log($"[{no} - Update request]: exception caugth.");
            }

        }

        private void HandleError(uint no)
        {
            Log($"[{no} - Message unknown]: no response");
        }

        private void RespondSend(string[] split, uint no, int id)
        {
            try
            {
                switch (split[1])
                {
                    case "client":
                        var client = _clientService.GetClientByID(id);
                        string msgC = "client#" + Serialization.Serialize(client);
                        _con.ws.SendTask(msgC);
                        Log($"[{no} - Send request]: responding - {msgC}");
                        break;

                    case "product":
                        var product = _productService.GetProductByID(id);
                        string msgP = "product#" + Serialization.Serialize(product);
                        _con.ws.SendTask(msgP);
                        Log($"[{no} - Send request]: responding - {msgP}");
                        break;

                    case "entry":
                        var evEntry = _evidenceEntryService.GetEvidenceEntryByID(id);
                        string msgE = "entry#" + Serialization.Serialize(evEntry);
                        _con.ws.SendTask(msgE);
                        Log($"[{no} - Send request]: responding - {msgE}");
                        break;

                    case "order":
                        var order = _clientService.GetClientByID(id);
                        string msgO = "order#" + Serialization.Serialize(order);
                        _con.ws.SendTask(msgO);
                        Log($"[{no} - Send request]: responding - {msgO}");
                        break;
                }
            }
            catch
            {
                Log($"[{no} - Send request]: no response, exception caugth.");
            }
        }

        private void RespondSendAll(string[] split, uint no)
        {
            try
            {
                switch (split[1])
                {
                    case "client":
                        var client = _clientService.GetAllClients();
                        string msgC = "client#" + Serialization.Serialize(client);
                        _con.ws.SendTask(msgC);
                        Log($"[{no} - Send request]: responding - {msgC}");
                        break;

                    case "product":
                        var product = _productService.GetAllProducts();
                        string msgP = "product#" + Serialization.Serialize(product);
                        _con.ws.SendTask(msgP);
                        Log($"[{no} - Send request]: responding - {msgP}");
                        break;

                    case "entry":
                        var evEntry = _evidenceEntryService.GetAllEvidenceEntries();
                        string msgE = "entry#" + Serialization.Serialize(evEntry);
                        _con.ws.SendTask(msgE);
                        Log($"[{no} - Send request]: responding - {msgE}");
                        break;

                    case "order":
                        var order = _clientService.GetAllClients();
                        string msgO = "order#" + Serialization.Serialize(order);
                        _con.ws.SendTask(msgO);
                        Log($"[{no} - Send request]: responding - {msgO}");
                        break;
                }
            }
            catch
            {
                Log($"[{no} - Send request]: no response, exception caugth.");
            }
        }

        private bool ParseId(string str, out int id, out bool all)
        {
            all = false;

            if (int.TryParse(str, out id))
                return true;

            if (str == "all")
                return all = true;

            return false;
        }
    }
}
