using LogicAPI;
using LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfApp.ViewModels;

namespace WpfApp.Commands
{
    class AddOrderCommand : ICommand
    {
        private readonly NewOrderViewModel _vm;
        private readonly IOrderService _service;

        public AddOrderCommand(NewOrderViewModel vm)
        {
            _vm = vm;
            _service = Logic.CreateOrderService();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {/*
            return _vm.Name != NewClientViewModel.defaultName
                && _vm.Adress != NewClientViewModel.defaultAddres
                && _vm.Name.Length > 1
                && _vm.Adress.Length > 1;*/
            return false;
        }

        public void Execute(object parameter)
        {
            _service.AddOrderDTO(_vm.CreateDTO());
            _vm.ResetFields();
        }
    }
}
