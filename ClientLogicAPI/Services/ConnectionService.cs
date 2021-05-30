using System;
using System.Threading.Tasks;
using Client.DataAPI;
using Client.LogicAPI.Interfaces;
using CommunicationAPI.Models;

namespace Client.LogicAPI.Services
{
    public class ConnectionService : IConnectionService
    {
        private WebSocketController socketController;
        private string _peer;

        public ConnectionService(string peer)
        {
            socketController = new WebSocketController();
            socketController.DataUpdate += OnDataUpdate;
            _peer = peer;
        }

        public async Task<bool> CreateConnection()
        {
            await socketController.Connect(_peer);
            return true;
        }

        public async Task CloseConnection()
        {
            await socketController.Disconnect();
        }

        public async Task<bool> SendTask(string newTask)
        {
            await socketController.SendTask(newTask);            
            return true;
        }

        private void OnDataUpdate(Type type)
        {
            if (type == typeof(CClient))
                Logic.InvokeClientsChanged();
            else if (type == typeof(CEvidenceEntry))
                Logic.InvokeEvidenceEntryChanged();
            else if (type == typeof(CProduct))
                Logic.InvokeProductsChanged();
            else if (type == typeof(COrder))
                Logic.InvokeOrdersChanged();
        }
    }
}
