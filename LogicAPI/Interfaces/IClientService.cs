using System;
using System.Collections.Generic;
using System.Text;
using LogicAPI.DTOs;

namespace LogicAPI.Interfaces
{
    public interface IClientService : IService
    {
        public bool ValidateModel(ClientDTO client);
        public bool AddClientDTO(ClientDTO client);
        public List<ClientDTO> GetAllClientDTOs();
        public ClientDTO GetClientDTOByID(int id);
        public ClientDTO GetClientDTOByName(string name);
    }
}
