﻿using Client.LogicAPI;
using Client.LogicAPI.DTOs;
using Client.LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Client.App.Commands;


namespace Client.App.ViewModels
{
    class OrdersViewModel : INotifyPropertyChanged
    {

        private IOrderService _orderService;
        public OrdersViewModel()
        {
            _orderService = Logic.CreateOrderService();
            Logic.OrdersChanged += OnOrdersChanged;
            Selected = Orders[0];
        }

        ~OrdersViewModel()
        {
            Logic.OrdersChanged -= OnOrdersChanged;
        }

        private OrderDTO _selected;
        private ObservableCollection<OrderDTO> _orders;

        public ObservableCollection<OrderDTO> Orders 
        { 
            get
            {
                int index;
                if (_selected != null)
                {
                    index = _orders.IndexOf(_selected);
                }
                else
                {
                    index = 0;
                }

                _orders = new ObservableCollection<OrderDTO>(_orderService.GetAllOrderDTOs());
                
                if (_orders.Count > index)
                {
                    _selected = _orders[index];
                }
                else
                {
                    _selected = _orders[^1];
                }

                return _orders;
            }
            set
            {
                _orders = value;
            }
        }

        public string OrderHeader => $"Order № {Selected.ID}: {Selected.ClientName} - {Selected.ClientAdress}";
        public string TotalPrice => $"Total price: {_orderService.GetPriceOfOrder(Selected)}";

        public OrderDTO Selected
        {
            get
            {
                if (_selected == null)
                    _selected = _orders[0];

                return _selected;
            }
            set
            {
                _selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Selected"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OrderHeader"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPrice"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnOrdersChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Orders"));
    }
}