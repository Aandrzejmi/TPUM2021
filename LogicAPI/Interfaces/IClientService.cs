using System;
using System.Collections.Generic;
using System.Text;
using DataAPI.DTOs;

namespace LogicAPI.Interfaces
{
    public interface IClientService : IService
    {
        public ClientDTO GetClientDTOByID(int id);
        public ClientDTO GetClientDTOByName(string name);
    }
}
