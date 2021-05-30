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
    class SubscribeCommand : ICommand
    {
        private ConnectionViewModel _vm;
        public event Action OnExecute;

        public SubscribeCommand(ConnectionViewModel vm)
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
            if (_vm.Subscribed)
            {
                Task.Run(async () =>
                {
                    await _connectionService.SendTask(Serialize(new CSubscribeUpdates(){ Subscribe = false}));
                    _vm.Subscribed = false;
                });
            }
            else
            {
                var task = Task.Run(async () =>
                {
                    await _connectionService.SendTask(Serialize(new CSubscribeUpdates() { Subscribe = true, CycleInSeconds = 10 }));
                    _vm.Subscribed = true;
                });
            }
        }
    }
}
