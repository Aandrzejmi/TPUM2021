using System;
using System.Collections.Generic;
using System.Text;
using DataAPI;
using LogicAPI.Interfaces;
using LogicAPI.Exceptions;
using DataAPI.DTOs;

namespace LogicAPI.Services
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
            if (_model is Client client)
            {
                if (client.ID < 0)
                    throw new ClientInvalidIDException();

                if (_repository.FindClientByID(client.ID) is null)
                    throw new ClientNotFoundException();

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

            if (_repository.FindClientByID(id) is Client client)
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

            if (_repository.FindClientByName(name) is Client client)
            {
                clientDTO.ID = client.ID;
                clientDTO.Name = client.Name;
                clientDTO.Adress = client.Adress;

                return clientDTO;
            }
            throw new ClientNotFoundException();
        }
    }
}
