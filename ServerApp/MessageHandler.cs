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
        private uint counter = 0;

        public MessageHandler(Connection con, Action<string> logFinction)
        {
            _con = con;
            Log = logFinction;
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
            catch
            {
                Log($"[{no} - Add request]: exception caugth.");
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
            catch
            {
                Log($"[{no} - Add request]: exception caugth.");
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
            catch
            {
                Log($"[{no} - Add request]: exception caugth.");
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
            catch
            {
                Log($"[{no} - Add request]: exception caugth.");
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
                    Log($"[{no} - Send request]: no response, exception caugth.");
                    return "Wrong request";
                }
            }
            catch
            {
                Log($"[{no} - Send request]: no response, exception caugth.");
                return "Wrong request";
            }
        }

        private string RespondSendAll(string typename, uint no)
        {
            try
            {
                try
                {
                    if (typeof(CClient).ToString().Equals(typename))
                    {
                        var client = _clientService.GetAllClients();
                        string msgC = "clientL#" + Serialize(client);
                        Log($"[{no} - Send request]: responding - {msgC}");
                        return msgC;
                    }
                    else if (typeof(CProduct).ToString().Equals(typename))
                    {
                        var product = _productService.GetAllProducts();
                        string msgP = "productL#" + Serialize(product);
                        Log($"[{no} - Send request]: responding - {msgP}");
                        return msgP;
                    }
                    else if (typeof(CEvidenceEntry).ToString().Equals(typename))
                    {
                        var evEntry = _evidenceEntryService.GetAllEvidenceEntries();
                        string msgE = "entryL#" + Serialize(evEntry);
                        Log($"[{no} - Send request]: responding - {msgE}");
                        return msgE;
                    }
                    else if (typeof(COrder).ToString().Equals(typename))
                    {
                        var order = _orderService.GetAllOrders();
                        string msgO = "orderL#" + Serialize(order);
                        Log($"[{no} - Send request]: responding - {msgO}");
                        return msgO;
                    }
                    else
                    {
                        Log($"[{no} - Send request]: no response, exception caugth.");
                        return "Wrong request";
                    }
                }
                catch
                {
                    Log($"[{no} - Send request]: no response, exception caugth.");
                    return "Wrong request";
                }
            }
            catch
            {
                Log($"[{no} - Send request]: no response, exception caugth.");
                return "Wrong request";
            }
        }
    }
}
