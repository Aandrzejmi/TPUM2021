﻿using System;
using System.Collections.Generic;
using System.Text;
using Server.LogicAPI.DTOs;

namespace Server.LogicAPI.Interfaces
{
    public interface IClientService : IService
    {
        public bool ValidateModel(ClientDTO client);
        public bool AddClientDTO(ClientDTO client);
        public bool ChangeClientDTO(int clientID, ClientDTO clientDTO);
        public List<ClientDTO> GetAllClientDTOs();
        public ClientDTO GetClientDTOByID(int id);
        public ClientDTO GetClientDTOByName(string name);
    }
}