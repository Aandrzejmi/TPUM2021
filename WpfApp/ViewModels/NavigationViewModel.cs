﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WpfApp.Commands;

namespace WpfApp.ViewModels
{
    class NavigationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _currentPage;

        //public ICommand SetMainPage { get; set; }
        public ICommand SetClientsPage { get; set; }
        public ICommand SetOrdersPage { get; set; }
        public ICommand SetProductsPage { get; set; }

        public NavigationViewModel()
        {
            SetClientsPage = new SetPageCommand(this, "Pages/ClientsPage.xaml");
            SetOrdersPage = new SetPageCommand(this, "Pages/OrdersPage.xaml");
            SetProductsPage = new SetPageCommand(this, "Pages/ProductsPage.xaml");
        }


        public string CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            }
        }

    }
}
