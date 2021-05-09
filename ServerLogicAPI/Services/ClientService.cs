using System.Collections.Generic;
using Server.DataAPI;
using Server.LogicAPI.Interfaces;
using Server.LogicAPI.Exceptions;
using CommunicationAPI.Models;

namespace Server.LogicAPI.Services
{
    internal class ClientService : IClientService
    {
        private readonly IRepository _repository;

        public ClientService(IRepository repository)
        {
            _repository = repository;
        }
        public bool ValidateModel(IModel _model)
        {
            if (_model is Client client)
            {
                if (client.ID < 0)
                    throw new ClientInvalidIDException();

                if (client.Name.Length == 0)
                    throw new ClientInvalidNameException();

                if (client.Adress.Length == 0)
                    throw new ClientInvalidAdressException();

                return true;
            }
            throw new ModelIsNotClientException();
        }

        public bool ValidateModel(CClient client)
        {
            if (client is CClient)
            {
                if (client.ID < 0)
                    throw new ClientInvalidIDException();

                if (client.Name.Length == 0)
                    throw new ClientInvalidNameException();

                if (client.Adress.Length == 0)
                    throw new ClientInvalidAdressException();

                return true;
            }
            throw new ModelIsNotClientException();
        }

        public CClient GetClientByID(int id)
        {
            var cClient = new CClient();

            if (_repository.FindClientByID(id) is Client client)
            {
                cClient.ID = client.ID;
                cClient.Name = client.Name;
                cClient.Adress = client.Adress;

                return cClient;
            }
            throw new ClientNotFoundException();
        }

        public CClient GetClientByName(string name)
        {
            var cClient = new CClient();

            if (_repository.FindClientByName(name) is Client client)
            {
                cClient.ID = client.ID;
                cClient.Name = client.Name;
                cClient.Adress = client.Adress;

                return cClient;
            }
            throw new ClientNotFoundException();
        }

        public List<CClient> GetAllClients()
        {
            List<CClient> cClients = new List<CClient>();
            foreach(Client client in _repository.GetAllClients())
            {
                cClients.Add(GetClientByID(client.ID));
            }
            return cClients;
        }

        public bool AddClient(CClient client)
        {
            if(ValidateModel(client))
            {
                List<CClient> cClients = GetAllClients();
                int newID = 0;
                foreach (CClient cClientListObject in cClients)
                {
                    if (newID == cClientListObject.ID)
                        newID++;
                    else
                        break;
                }

                var clientModel = new Client();
                clientModel.ID = newID;
                clientModel.Name = client.Name;
                clientModel.Adress = client.Adress;

                if (_repository.AddClient(clientModel))
                {
                    Logic.InvokeClientsChanged();
                    return true;
                }
            }
            return false;
        }

        public bool ChangeClient(int clientID, CClient cClient)
        {
            if (_repository.FindClientByID(clientID) is Client client)
            {   
                if (ValidateModel(cClient))
                {
                    client.Adress = cClient.Adress;
                    client.Name = cClient.Name;
                    if (_repository.ModifyClient(client))
                    {
                        Logic.InvokeClientsChanged();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new ClientNotFoundException();
            }
        }
    }
}
