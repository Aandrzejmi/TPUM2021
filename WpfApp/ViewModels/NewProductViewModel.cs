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
    class NewProductViewModel : INotifyPropertyChanged
    {
        public const string defaultName = "Enter name";
        public ICommand Add { get; set; }

        private string _name;
        private decimal _price;
        private int _amount;

        public NewProductViewModel()
        {
            Add = new AddProductCommand(this);
            ResetFields();
        }

        public EvidenceEntryDTO CreateDTO()
        {
            return new EvidenceEntryDTO()
            {
                Name = _name,
                Price = _price,
                ProductAmount = _amount,
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
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Amount"));
            }
        }

        public void ResetFields()
        {
            Name = defaultName;
            Price = 0.0m;
            Amount = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
