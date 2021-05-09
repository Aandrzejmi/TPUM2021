using CommunicationAPI;
using CommunicationAPI.Models;
using Server.LogicAPI;
using Server.LogicAPI.Interfaces;
using Server.LogicAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.App
{
    class MessageHandler
    {
        private readonly IClientService _clientService = Logic.CreateClientService();
        private readonly IProductService _productService = Logic.CreateProductService();
        private readonly IEvidenceEntryService _evidenceEntryService = Logic.CreateEvidenceEntryService();
        private readonly IOrderService _orderService = Logic.CreateOrderService();
        private readonly WebSocketConnection socket;
        private readonly Action<string> Log;
        private uint counter = 0;

        public MessageHandler(WebSocketConnection ws, Action<string> logFinction)
        {
            socket = ws;
            Log = logFinction;
        }

        public void Handle(string data)
        {
            Task.Run(async () =>
            {
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
            
            if (!parseId(split[2], out id, out all))
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
            int id;
            bool all;

            if (!parseId(split[1], out id, out all))
            {
                HandleError(no);
                return;
            }

            Log($"[{no} - Update request]: ccc");

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
                        socket.SendTask(msgC);
                        Log($"[{no} - Send request]: responding - {msgC}");
                        break;

                    case "product":
                        var product = _productService.GetProductByID(id);
                        string msgP = "product#" + Serialization.Serialize(product);
                        socket.SendTask(msgP);
                        Log($"[{no} - Send request]: responding - {msgP}");
                        break;

                    case "entry":
                        var evEntry = _evidenceEntryService.GetEvidenceEntryByID(id);
                        string msgE = "entry#" + Serialization.Serialize(evEntry);
                        socket.SendTask(msgE);
                        Log($"[{no} - Send request]: responding - {msgE}");
                        break;

                    case "order":
                        var order = _clientService.GetClientByID(id);
                        string msgO = "order#" + Serialization.Serialize(order);
                        socket.SendTask(msgO);
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
                        var clients = _clientService.GetAllClients();
                        if (clients.Count == 0)
                        {
                            Log($"[{no} - Send request]: no response, clients repository is empty.");
                        }
                        break;
                }
            }
            catch
            {
                Log($"[{no} - Send request]: no response, exception caugth.");
            }
        }

        private void RespondAdd(string[] split, uint no)
        {

        }

        private void RespondUpdate(string[] split, uint no, int id)
        {

        }

        private bool parseId(string str, out int id, out bool all)
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
