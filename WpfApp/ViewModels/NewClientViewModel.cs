using Server.LogicAPI;
using Server.LogicAPI.DTOs;
using Server.LogicAPI.Interfaces;
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

        private string _name;
        private string _adress;

        public ICommand Add { get; set; }

        public NewClientViewModel()
        {
            Add = new AddClientCommand(this);
            ResetFields();
        }

        public ClientDTO CreateDTO()
        {
            return new ClientDTO()
            {
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
