using System.Collections.Generic;
using Client.LogicAPI.DTOs;
using CommunicationAPI.Models;

namespace Client.LogicAPI.Interfaces
{
    public interface IClientService
    {
        public bool ValidateModel(ClientDTO client);
        public bool ValidateModel(CClient _model);
        public bool AddClientDTO(ClientDTO client);
        public bool ChangeClientDTO(int clientID, ClientDTO clientDTO);
        public List<ClientDTO> GetAllClientDTOs();
        public ClientDTO GetClientDTOByID(int id);
        public ClientDTO GetClientDTOByName(string name);
    }
}
