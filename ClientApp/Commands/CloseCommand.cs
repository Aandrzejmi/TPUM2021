using Client.App.ViewModels;
using Client.LogicAPI;
using Client.LogicAPI.DTOs;
using Client.LogicAPI.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.App.Commands
{
    class CloseCommand : ICommand
    {
        private ConnectionViewModel _vm;
        public event Action OnExecute;

        public CloseCommand(ConnectionViewModel vm)
        {
            _vm = vm;
        }

        private IConnectionService _connectionService = Logic.CreateConnectionService();

        public event EventHandler CanExecuteChanged;
        public bool IsActive { get; set; } = false;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _connectionService.CloseConnection();
            _vm.Connected = false;
        }
    }
}
