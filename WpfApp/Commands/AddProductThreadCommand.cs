using LogicAPI;
using LogicAPI.DTOs;
using LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace WpfApp.Commands
{
    class AddProductThreadCommand : ICommand
    {
        private static int index = 0;
        private IProductService _productService = Logic.CreateProductService();
        private Thread _thread;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (_thread == null)
            {
                _thread = new Thread(AddProductsLoop);
                _thread.Start();
            }
            else
            {
                _thread.Abort();
                _thread = null;
            }
        }

        private ProductDTO CreateDTO() => new ProductDTO() { Price = 2.0M, Name = $"New product {index++}" };

        private void AddProductsLoop()
        {
            while (true)
            {
                _productService.AddProductDTO(CreateDTO());
                Thread.Sleep(30000);
            }
        }
    }
}
