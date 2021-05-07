using Server.LogicAPI;
using Server.LogicAPI.DTOs;
using Server.LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Moq;

namespace WpfApp.ViewModels
{
    class ClientsViewModel : INotifyPropertyChanged
    {
        private IClientService _clientService;

        public ClientsViewModel()
        {
            _clientService = Logic.CreateClientService();
            Logic.ClientsChanged += OnClientsChanged;
        }

        ~ClientsViewModel()
        {
            Logic.ClientsChanged -= OnClientsChanged;
        }

        public ObservableCollection<ClientDTO> Clients => new ObservableCollection<ClientDTO>(_clientService.GetAllClientDTOs());

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnClientsChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Clients"));

    }
}