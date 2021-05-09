using Client.LogicAPI;
using Client.LogicAPI.DTOs;
using Client.LogicAPI.Interfaces;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System.Diagnostics;

namespace Client.App.Commands
{
    class AddClientsThreadCommand : ICommand
    {
        public event Action OnExecute;

        private static int index = 0;
        private IClientService _clientService = Logic.CreateClientService();
        private Thread _thread;
        private Action<string> connectionLogger = (x) => Debug.WriteLine(x);
        private IConnectionService _connectionService = Logic.CreateConnectionService();
        public event EventHandler CanExecuteChanged;
        public bool IsActive { get; set; } = false;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            Task.Run(() => _connectionService.CreateConnection());
            
            //if (_thread == null)
            //{
            //    _thread = new Thread(AddClientsLoop);
            //    _thread.Start();
            //}

            //IsActive = !IsActive;
            //OnExecute?.Invoke();
        }


        private ClientDTO CreateDTO() => new ClientDTO() { Adress = "Temporary Adress", Name = $"New client {index++}" };

        private void AddClientsLoop()
        {
            while (true)
            {
                while (!IsActive) { }

                _clientService.AddClientDTO(CreateDTO());
                Thread.Sleep(5000);
            }
        }
    }
}
