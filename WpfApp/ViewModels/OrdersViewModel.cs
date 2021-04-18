using LogicAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WpfApp.Commands;


namespace WpfApp.ViewModels
{
    class OrdersViewModel : INotifyPropertyChanged
    {
        public OrdersViewModel()
        {
            Selected = Orders[0];
        }

        private OrderDTO _selected;

        public ObservableCollection<OrderDTO> Orders { get; set; } = new ObservableCollection<OrderDTO>()
        {
            new OrderDTO()
            { 
                ID = 1,
                Client = new ClientDTO(){ID = 1, Name = "Jan Kowalski", Adress = "Południowa 23"},
                Products = new List<EvidenceEntryDTO>()
                {
                    new EvidenceEntryDTO(){ Product = new ProductDTO() {ID = 1, Name = "Kawa", Price = 5.20M }, ProductAmount = 2 },
                }
            },
            new OrderDTO()
            {
                ID = 2,
                Client = new ClientDTO(){ID = 1, Name = "Jan Kowalski", Adress = "Południowa 23"},
                Products = new List<EvidenceEntryDTO>()
                {
                    new EvidenceEntryDTO(){ Product = new ProductDTO() {ID = 2, Name = "Ciastka", Price = 4.20M }, ProductAmount = 2 },
                    new EvidenceEntryDTO(){ Product = new ProductDTO() {ID = 3, Name = "Ładowarka USB C", Price = 15.50M }, ProductAmount = 1}
                }
            },
            new OrderDTO()
            {
                ID = 3,
                Client = new ClientDTO(){ID = 3, Name = "Alicja Makota ", Adress = "Długa 281 mieszkania 12"},
                Products = new List<EvidenceEntryDTO>()
                {
                    new EvidenceEntryDTO(){ Product = new ProductDTO() {ID = 4, Name = "Jabłka (kg)", Price = 1.20M }, ProductAmount = 3},
                    new EvidenceEntryDTO(){ Product = new ProductDTO() {ID = 5, Name = "Sliwki (kg)", Price = 4.50M }, ProductAmount = 4},
                    new EvidenceEntryDTO(){ Product = new ProductDTO() {ID = 6, Name = "Lampka nocna", Price = 65.99M }, ProductAmount = 1},

                }
            },
        };

        public string OrderHeader => $"Order № {Selected.ID}: {Selected.ClientName} - {Selected.ClientAdress}";
        public string TotalPrice => $"Total price: [będzie, jak dodam wsparcie dla Serviceów]";

        public OrderDTO Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Selected"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OrderHeader"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPrice"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
