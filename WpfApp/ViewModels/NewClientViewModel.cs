using LogicAPI;
using LogicAPI.DTOs;
using LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WpfApp.Commands;

namespace WpfApp.ViewModels
{
    class NewClientViewModel : INotifyPropertyChanged
    {
        public const string defaultName = "Enter name";
        public const string defaultAddres = "Enter adress";

        private IClientService _clientService;
        private string _name;
        private string _adress;

        public ICommand Add { get; set; }

        public NewClientViewModel()
        {
            _clientService = Logic.CreateClientService();
            Add = new AddClientCommand(this, _clientService);
            ResetFields();
        }

        public ClientDTO CreateDTO()
        {
            return new ClientDTO()
            {
                ID = (new Random()).Next(1, int.MaxValue),
                Name = _name,
                Adress = _adress,
            };
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public string Adress
        {
            get => _adress;
            set
            {
                _adress = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Adress"));
            }
        }

        public void ResetFields()
        {
            Name = defaultName;
            Adress = defaultAddres;
        }

        public event PropertyChangedEventHandler PropertyChanged;


    }
}
