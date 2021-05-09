using System.Collections.Generic;
using Client.DataAPI;
using Client.LogicAPI.Interfaces;
using Client.LogicAPI.Exceptions;
using Client.LogicAPI.DTOs;
using CommunicationAPI.Models;
using static CommunicationAPI.Serialization;
using System;
using System.Threading.Tasks;

namespace Client.LogicAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository _repository;
        private IConnectionService connectionService;
        public ClientService(IRepository repository)
        {
            _repository = repository;
            connectionService = Logic.CreateConnectionService();
        }
        public bool ValidateModel(CClient _model)
        {
            if (_model is CClient client)
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

        public bool ValidateModel(ClientDTO client)
        {
            if (client is ClientDTO)
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

        public ClientDTO GetClientDTOByID(int id)
        {
            var clientDTO = new ClientDTO();

            if (_repository.FindClientByID(id) is CClient client)
            {
                clientDTO.ID = client.ID;
                clientDTO.Name = client.Name;
                clientDTO.Adress = client.Adress;

                return clientDTO;
            }
            throw new ClientNotFoundException();
        }

        public ClientDTO GetClientDTOByName(string name)
        {
            var clientDTO = new ClientDTO();

            if (_repository.FindClientByName(name) is CClient client)
            {
                clientDTO.ID = client.ID;
                clientDTO.Name = client.Name;
                clientDTO.Adress = client.Adress;

                return clientDTO;
            }
            throw new ClientNotFoundException();
        }

        public List<ClientDTO> GetAllClientDTOs()
        {
            List<ClientDTO> clientDTOs = new List<ClientDTO>();
            foreach(CClient client in _repository.GetAllClients())
            {
                clientDTOs.Add(GetClientDTOByID(client.ID));
            }
            return clientDTOs;
        }

        public bool AddClientDTO(ClientDTO client)
        {
            if(ValidateModel(client))
            {
                List<ClientDTO> clientDTOs = GetAllClientDTOs();
                int newID = 0;
                foreach (ClientDTO clientDTOListObject in clientDTOs)
                {
                    if (newID == clientDTOListObject.ID)
                        newID++;
                    else
                        break;
                }

                var clientModel = new CClient();
                clientModel.ID = newID;
                clientModel.Name = client.Name;
                clientModel.Adress = client.Adress;

                if (_repository.AddClient(clientModel))
                {
                    connectionService.SendTask("add#client#" + Serialize<CClient>(clientModel));
                    Logic.InvokeClientsChanged();
                    return true;
                }

               
            }
            return false;
        }

        public bool ChangeClientDTO(int clientID, ClientDTO clientDTO)
        {
            if (_repository.FindClientByID(clientID) is CClient client)
            {   
                if (ValidateModel(clientDTO))
                {
                    client.Adress = clientDTO.Adress;
                    client.Name = clientDTO.Name;
                    if (_repository.ModifyClient(client))
                    {
                        connectionService.SendTask($"update#client#{client.ID}#{Serialize<CClient>(client)}");
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
