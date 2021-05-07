using Server.LogicAPI;
using Server.LogicAPI.DTOs;
using Server.LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace WpfApp.Commands
{
    class AddClientsThreadCommand : ICommand
    {
        public event Action OnExecute;

        private static int index = 0;
        private IClientService _clientService = Logic.CreateClientService();
        private Thread _thread;

        public event EventHandler CanExecuteChanged;
        public bool IsActive { get; set; } = false;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (_thread == null)
            {
                _thread = new Thread(AddClientsLoop);
                _thread.Start();
            }

            IsActive = !IsActive;
            OnExecute?.Invoke();
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
