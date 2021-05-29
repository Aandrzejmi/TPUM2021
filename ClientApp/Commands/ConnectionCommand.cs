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
            if (_vm._connected)
            {
                Task.Run(async () =>
                {
                    _vm.Connected = false;
                    await _connectionService.CloseConnection();
                });
            }
            else
            {
                var task = Task.Run(async () =>
                {
                    await _connectionService.CreateConnection();
                    var task1 = _connectionService.SendTask(Serialize(new CSendRequest()
                        { Type = typeof(CProduct).ToString(), RequestedID = null }));
                    var task2 = _connectionService.SendTask(Serialize(new CSendRequest()
                        { Type = typeof(COrder).ToString(), RequestedID = null }));
                    var task3 = _connectionService.SendTask(Serialize(new CSendRequest()
                        { Type = typeof(CEvidenceEntry).ToString(), RequestedID = null }));
                    var task4 = _connectionService.SendTask(Serialize(new CSendRequest()
                        { Type = typeof(CClient).ToString(), RequestedID = null }));
                    await task1;
                    await task2;
                    await task3;
                    await task4;
                });
            }
        }
    }
}
