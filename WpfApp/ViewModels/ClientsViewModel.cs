using LogicAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;


namespace WpfApp.ViewModels
{
    class ClientsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ClientDTO> Clients { get; set; } = new ObservableCollection<ClientDTO>()
        {
            new ClientDTO(){ID = 1, Name = "Jan Kowalski", Adress = "Południowa 23"},
            new ClientDTO(){ID = 2, Name = "Anna Nowak", Adress = "Złota 15b"},
            new ClientDTO(){ID = 3, Name = "Alicja Makota ", Adress = "Długa 281 mieszkania 12"},
        };

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
