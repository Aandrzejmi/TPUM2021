using LogicAPI;
using LogicAPI.Exceptions;
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
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public AddOrderCommand(NewOrderViewModel vm)
        {
            _vm = vm;
            _orderService = Logic.CreateOrderService();
            _productService = Logic.CreateProductService();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            var splitIDs = _vm.ProductsIDs.Split();
            var splitAmounts = _vm.ProductsAmounts.Split();

            if (splitAmounts.Length != splitIDs.Length)
                return false;

            int len = splitIDs.Length;

            var ids = new int[len];
            var amo = new int[len];

            for (int i = 0; i < splitIDs.Length; i++)
            {
                if (!int.TryParse(splitIDs[i], out ids[i]))
                    return false;

                if (!int.TryParse(splitAmounts[i], out amo[i]))
                    return false;

                try { _productService.GetProductDTOByID(ids[i]); }
                catch (ProductNotFoundException)
                    { return false; }

                if (amo[i] <= 0)
                    return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            _orderService.AddOrderDTO(_vm.CreateDTO());
            _vm.ResetFields();
        }
    }
}
