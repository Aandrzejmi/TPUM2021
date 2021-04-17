using System;
using System.Collections.Generic;
using System.Text;
using LogicAPI.DTOs;

namespace LogicAPI.Interfaces
{
    public interface IClientService : IService
    {
        public List<ClientDTO> GetAllClientDTOs();
        public ClientDTO GetClientDTOByID(int id);
        public ClientDTO GetClientDTOByName(string name);
    }
}
