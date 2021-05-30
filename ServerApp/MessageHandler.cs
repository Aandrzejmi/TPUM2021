using CommunicationAPI;
using CommunicationAPI.Models;
using static CommunicationAPI.Serialization;
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
        private readonly Func<string, Task> SendMessage;
        private uint counter = 0;

        public MessageHandler(Connection con, Action<string> logFunction, Func<string, Task> sendFunction)
        {
            _con = con;
            Log = logFunction;
            SendMessage = sendFunction;
        }

        public string Handle(string data)
        {
            _con.timer.Reset();

            uint no = counter++;
            Log($"[Received message {no}]: {data}");

            if (data.StartsWith("{\"__type\":\"CSendRequest"))
            {
                return HandleSend(Deserialize<CSendRequest>(data), no);
            }
            if (data.StartsWith("{\"__type\":\"CSubscribeUpdates"))
            {
                HandleSubscribe(Deserialize<CSubscribeUpdates>(data), no);
                return "Request done";
            }
            else if (data.StartsWith("{\"__type\":\"CClient"))
            {
                HandleClient(Deserialize<CClient>(data), no);
                return "Request done";
            }
            else if (data.StartsWith("{\"__type\":\"CProduct"))
            {
                HandleProduct(Deserialize<CProduct>(data), no);
                return "Request done";
            }
            else if (data.StartsWith("{\"__type\":\"CEvidenceEntry"))
            {
                HandleEvEntry(Deserialize<CEvidenceEntry>(data), no);
                return "Request done";
            }
            else if (data.StartsWith("{\"__type\":\"COrder"))
            {
                HandleOrder(Deserialize<COrder>(data), no);
                return "Request done";
            }
            else
            {
                return HandleError(no);
            }
        }

        private string HandleSend(CSendRequest request, uint no)
        {
            if (request.RequestedID.HasValue)
                return RespondSend(request.Type, request.RequestedID.Value, no);
            else
                return RespondSendAll(request.Type, no);

        }

        private void HandleSubscribe(CSubscribeUpdates request, uint no)
        {
            if (request.Subscribe)
            {
                if (_con.updateObserver != null)
                {
                    //todo unsubscribe
                }

                _con.updateObserver = new SubscribeUpdateObserver(SendMessage, Log, request.CycleInSeconds);
                Log($"[{no} - Subscribe request]: Subscribed to updates");
            }
            else //(Unsubscribe)
            {
                //todo unsubscribe
                Log($"[{no} - Subscribe request]: Unsubscribed from updates");
            }
        }

        private void HandleClient(CClient model, uint no)
        {
            try
            {
                if (model.ID < 0)
                {
                    model.ID = 0;
                    _clientService.AddClient(model);
                    Log($"[{no} - Add request]: client added");
                }
                else
                {
                    _clientService.ChangeClient(model.ID, model);
                    Log($"[{no} - Update request]: client updated");
                }
            }
            catch (Exception e)
            {
                Log($"[{no} - Add/Update request]: exception caugth: {e}");
            }
        }

        private void HandleProduct(CProduct model, uint no)
        {
            try
            {
                if (model.ID < 0)
                {
                    model.ID = 0;
                    _productService.AddProduct(model);
                    Log($"[{no} - Add request]: client added");
                }
                else
                {
                    _productService.ChangeProduct(model.ID, model);
                    Log($"[{no} - Update request]: client updated");
                }
            }
            catch (Exception e)
            {
                Log($"[{no} - Add/Update request]: exception caugth: {e}");
            }
        }

        private void HandleEvEntry(CEvidenceEntry model, uint no)
        {
            try
            {
                if (model.Product.ID < 0)
                {
                    model.Product.ID = 0;
                    _evidenceEntryService.AddEvidenceEntry(model);
                    Log($"[{no} - Add request]: client added");
                }
                else
                {
                    _evidenceEntryService.ChangeEvidenceEntry(model.Product.ID, model);
                    Log($"[{no} - Update request]: client updated");
                }
            }
            catch (Exception e)
            {
                Log($"[{no} - Add/Update request]: exception caugth: {e}");
            }
        }

        private void HandleOrder(COrder model, uint no)
        {
            try
            {
                if (model.ID < 0)
                {
                    model.ID = 0;
                    _orderService.AddOrder(model);
                    Log($"[{no} - Add request]: client added");
                }
                else
                {
                    _orderService.ChangeOrder(model.ID, model);
                    Log($"[{no} - Update request]: client updated");
                }
            }
            catch (Exception e)
            {
                Log($"[{no} - Add/Update request]: exception caugth: {e}");
            }
        }

        private string HandleError(uint no)
        {
            Log($"[{no} - Message unknown]: no response");
            return "Wrong request";
        }

        private string RespondSend(string typename, int id, uint no)
        {
            try
            {
                if (typeof(CClient).ToString().Equals(typename))
                {
                    var client = _clientService.GetClientByID(id);
                    string msgC = "client#" + Serialize(client);
                    Log($"[{no} - Send request]: responding - {msgC}");
                    return msgC;
                }
                else if (typeof(CProduct).ToString().Equals(typename))
                {
                    var product = _productService.GetProductByID(id);
                    string msgP = "product#" + Serialize(product);
                    Log($"[{no} - Send request]: responding - {msgP}");
                    return msgP;
                }
                else if (typeof(CEvidenceEntry).ToString().Equals(typename))
                {
                    var evEntry = _evidenceEntryService.GetEvidenceEntryByID(id);
                    string msgE = "entry#" + Serialize(evEntry);
                    Log($"[{no} - Send request]: responding - {msgE}");
                    return msgE;
                }
                else if (typeof(COrder).ToString().Equals(typename))
                {
                    var order = _clientService.GetClientByID(id);
                    string msgO = "order#" + Serialize(order);
                    Log($"[{no} - Send request]: responding - {msgO}");
                    return msgO;
                }
                else
                {
                    Log($"[{no} - Send request]: no response, Unknown type: {typename}");
                    return "Wrong request";
                }
            }
            catch (Exception e)
            {
                Log($"[{no} - Send request]: no response, exception caugth : {e}");
                return "Wrong request";
            }
        }

        private string RespondSendAll(string typename, uint no)
        {
            try
            {
                if (typeof(CClient).ToString().Equals(typename))
                {
                    var clients = _clientService.GetAllClients();
                    string msgC = Serialize(clients);
                    Log($"[{no} - Send request]: responding - {msgC}");
                    return msgC;
                }
                else if (typeof(CProduct).ToString().Equals(typename))
                {
                    var products = _productService.GetAllProducts();
                    string msgP = Serialize(products);
                    Log($"[{no} - Send request]: responding - {msgP}");
                    return msgP;
                }
                else if (typeof(CEvidenceEntry).ToString().Equals(typename))
                {
                    var evEntries = _evidenceEntryService.GetAllEvidenceEntries();
                    string msgE = Serialize(evEntries);
                    Log($"[{no} - Send request]: responding - {msgE}");
                    return msgE;
                }
                else if (typeof(COrder).ToString().Equals(typename))
                {
                    var orders = _orderService.GetAllOrders();
                    string msgO = Serialize(orders);
                    Log($"[{no} - Send request]: responding - {msgO}");
                    return msgO;
                }
                else
                {
                    Log($"[{no} - Send request]: no response, Unknown type: {typename}");
                    return "Wrong request";
                }
            }
            catch (Exception e)
            {
                Log($"[{no} - Send request]: no response, exception caugth : {e}");
                return "Wrong request";
            }
        }
    }
}
