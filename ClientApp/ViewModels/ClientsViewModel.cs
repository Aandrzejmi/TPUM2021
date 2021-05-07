using Client.LogicAPI;
using Client.LogicAPI.DTOs;
using Client.LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Moq;

namespace Client.App.ViewModels
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