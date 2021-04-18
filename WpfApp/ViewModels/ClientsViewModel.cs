using LogicAPI;
using LogicAPI.DTOs;
using LogicAPI.Interfaces;
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
        private ObservableCollection<ClientDTO> _clients;

        public ClientsViewModel()
        {
            _clientService = Logic.CreateClientService();
            // Logic.CreateClientService()
        }

        public ObservableCollection<ClientDTO> Clients 
        {
            get
            {
                _clients = new ObservableCollection<ClientDTO>(_clientService.GetAllClientDTOs());
                return _clients;
            }
            set
            {
                _clients = value;
                // _clientService.???
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}