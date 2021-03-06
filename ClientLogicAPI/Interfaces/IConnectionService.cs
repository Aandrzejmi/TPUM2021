using System.Threading.Tasks;

namespace Client.LogicAPI.Interfaces
{
    public interface IConnectionService
    {
        public Task<bool> CreateConnection();
        public Task CloseConnection();
        public Task<bool> SendTask(string newTask);

    }
}
