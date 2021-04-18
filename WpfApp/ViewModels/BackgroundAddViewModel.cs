﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WpfApp.Commands;

namespace WpfApp.ViewModels
{
    class BackgroundAddViewModel : INotifyPropertyChanged
    {
        public AddClientsThreadCommand AddClients { get; set; } = new AddClientsThreadCommand();
        public AddClientsThreadCommand AddOrders { get; set; } = new AddClientsThreadCommand();
        public AddClientsThreadCommand AddProducts { get; set; } = new AddClientsThreadCommand();

        public string ClientsLabel => AddClients.IsActive ? "Stop clients" : "Start clients";
        public string OrderssLabel => AddOrders.IsActive ? "Stop orders" : "Start orders";
        public string ProductsLabel => AddProducts.IsActive ? "Stop products" : "Start products";

        public event PropertyChangedEventHandler PropertyChanged;

        private void ClientsPressed() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ClientsLabel"));
        private void OrdersPressed() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OrderssLabel"));
        private void ProductsPressed() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProductsLabel"));

        public BackgroundAddViewModel()
        {
            AddClients.OnExecute += ClientsPressed;
            AddOrders.OnExecute += OrdersPressed;
            AddProducts.OnExecute += ProductsPressed;
        }

        ~BackgroundAddViewModel()
        {
            AddClients.OnExecute -= ClientsPressed;
            AddOrders.OnExecute -= OrdersPressed;
            AddProducts.OnExecute -= ProductsPressed;
        }
    }
}