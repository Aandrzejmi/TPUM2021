using System;
using System.Collections.Generic;
using System.Text;
using DataAPI;
using LogicAPI.Interfaces;
using LogicAPI.Exceptions;

namespace LogicAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository _repository;

        public ClientService()
        {

        }
        public bool ValidateModel(IModel _model)
        {
            if (_model is Client client)
            {
                if (client.ID < 0)
                    throw new ClientInvalidIDException();

                if (_repository.FindClientByID(0) is null)
                    throw new ClientNotFoundException();

                if (client.Name.Length == 0)
                    throw new ClientInvalidNameException();

                if (client.Adress.Length == 0)
                    throw new ClientInvalidAdressException();

                return true;
            }
            throw new ModelIsNotClientException();
        }
    }
}
