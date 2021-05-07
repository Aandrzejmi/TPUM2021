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
    class NewOrderViewModel : INotifyPropertyChanged
    {
        private IClientService _clientService;
        private IProductService _productService;

        private int _clientID;
        private string _productsIDs;
        private string _productsAmounts;

        public ICommand Add { get; set; }

        public NewOrderViewModel()
        {
            _clientService = Logic.CreateClientService();
            _productService = Logic.CreateProductService();

            Add = new AddOrderCommand(this);
            ResetFields();
        }

        public OrderDTO CreateDTO()
        {
            var products = new List<EvidenceEntryDTO>();

            var ids = new List<int>();
            var amo = new List<int>();

            foreach (var item in _productsIDs.Split())
                ids.Add(int.Parse(item));

            foreach (var item in _productsAmounts.Split())
                amo.Add(int.Parse(item));

            for (int i = 0; i < ids.Count; i++)
            {
                products.Add(new EvidenceEntryDTO()
                {
                    Product = _productService.GetProductDTOByID(ids[i]),
                    ProductAmount = amo[i],
                });
            }

            return new OrderDTO()
            {
                Client = _clientService.GetClientDTOByID(_clientID),
                Products = products,
            };
        }

        public int ClientID
        {
            get => _clientID;
            set
            {
                _clientID = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ClientID"));
            }
        }

        public string ProductsIDs
        {
            get => _productsIDs;
            set
            {
                _productsIDs = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProductsIDs"));
            }
        }

        public string ProductsAmounts
        {
            get => _productsAmounts;
            set
            {
                _productsAmounts = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProductsAmounts"));
            }
        }

        public void ResetFields()
        {
            ClientID = 0;
            ProductsIDs = "";
            ProductsAmounts = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}