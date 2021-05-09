using System.Collections.Generic;
using CommunicationAPI.Models;

namespace Server.LogicAPI.Interfaces
{
    public interface IClientService : IService
    {
        public bool ValidateModel(CClient client);
        public bool AddClient(CClient client);
        public bool ChangeClient(int clientID, CClient clientDTO);
        public List<CClient> GetAllClients();
        public CClient GetClientByID(int id);
        public CClient GetClientByName(string name);
    }
}
