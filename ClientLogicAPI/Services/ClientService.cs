using System;
using System.Collections.Generic;
using System.Text;
using Client.DataAPI;
using Client.LogicAPI;
using Client.LogicAPI.Interfaces;
using Client.LogicAPI.Exceptions;
using Client.LogicAPI.DTOs;

namespace Client.LogicAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository _repository;

        public ClientService(IRepository repository)
        {
            _repository = repository;
        }
        public bool ValidateModel(IModel _model)
        {
            if (_model is Client.DataAPI.Client client)
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

            if (_repository.FindClientByID(id) is Client.DataAPI.Client client)
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

            if (_repository.FindClientByName(name) is Client.DataAPI.Client client)
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
            foreach(Client.DataAPI.Client client in _repository.GetAllClients())
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

                var clientModel = new Client.DataAPI.Client();
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

        public bool ChangeClientDTO(int clientID, ClientDTO clientDTO)
        {
            if (_repository.FindClientByID(clientID) is Client.DataAPI.Client client)
            {   
                if (ValidateModel(clientDTO))
                {
                    client.Adress = clientDTO.Adress;
                    client.Name = clientDTO.Name;
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
