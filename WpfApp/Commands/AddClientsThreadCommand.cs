using LogicAPI;
using LogicAPI.DTOs;
using LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace WpfApp.Commands
{
    class AddClientsThreadCommand : ICommand
    {
        private static int index = 0;
        private IClientService _clientService = Logic.CreateClientService();
        private Thread _thread;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (_thread == null)
            {
                _thread = new Thread(AddClientsLoop);
                _thread.Start();
            }
            else
            {
                _thread.Abort();
                _thread = null;
            }
        }

        private ClientDTO CreateDTO() => new ClientDTO() { Adress = "Temporary Adress", Name = $"New client {index++}" };

        private void AddClientsLoop()
        {
            while (true)
            {
                _clientService.AddClientDTO(CreateDTO());
                Thread.Sleep(30000);
            }
        }
    }
}
