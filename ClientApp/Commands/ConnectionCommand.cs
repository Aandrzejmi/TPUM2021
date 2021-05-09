using Client.App.ViewModels;
using Client.LogicAPI;
using Client.LogicAPI.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

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
                    await _connectionService.CloseConnection();
                    _vm.Connected = false;
                });
            }
            else
            {
                Task.Run(async () => _vm.Connected = await _connectionService.CreateConnection());
            }
        }
    }
}
