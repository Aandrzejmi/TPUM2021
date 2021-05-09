using System.Threading.Tasks;
using Client.DataAPI;
using Client.LogicAPI.Interfaces;

namespace Client.LogicAPI.Services
{
    public class ConnectionService : IConnectionService
    {
        private WebSocketController socketController;
        private string _peer;

        public ConnectionService(string peer)
        {
            socketController = new WebSocketController();
            _peer = peer;
        }

        public async Task<bool> CreateConnection()
        {
            await socketController.Connect(_peer);
            return true;
        }

        public async Task<bool> SendTask(string newTask)
        {
            await socketController.SendTask(newTask);            
            return true;
        }
    }
}
