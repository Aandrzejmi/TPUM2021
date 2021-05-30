using Client.App.ViewModels;
using Client.LogicAPI;
using Client.LogicAPI.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunicationAPI.Models;
using static CommunicationAPI.Serialization;

namespace Client.App.Commands
{
    class ConnectionCommand : ICommand
    {
        private ConnectionViewModel _vm;
        public event Action OnExecute;

        public ConnectionCommand(ConnectionViewModel vm)
        {
            _vm = vm;
        }

        private IConnectionService _connectionService = Logic.CreateConnectionService();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (_vm.Connected)
            {
                Task.Run(async () =>
                {
                    await _connectionService.CloseConnection();
                    _vm.Connected = false;
                });
            }
            else
            {
                var task = Task.Run(async () =>
                {
                    var tasks = new Task[5];

                    await _connectionService.CreateConnection();
                    _vm.Connected = true;

                    tasks[0] = _connectionService.SendTask(Serialize(new CSendRequest()
                        { Type = typeof(CProduct).ToString(), RequestedID = null }));
                    tasks[1] = _connectionService.SendTask(Serialize(new CSendRequest()
                        { Type = typeof(COrder).ToString(), RequestedID = null }));
                    tasks[2] = _connectionService.SendTask(Serialize(new CSendRequest()
                        { Type = typeof(CEvidenceEntry).ToString(), RequestedID = null }));
                    tasks[3] = _connectionService.SendTask(Serialize(new CSendRequest()
                        { Type = typeof(CClient).ToString(), RequestedID = null }));
                    tasks[4] = _connectionService.SendTask(Serialize(new CSubscribeUpdates()
                        { Subscribe = true, CycleInSeconds = 10 }));

                    Task.WaitAll(tasks);
                });
            }
        }
    }
}
