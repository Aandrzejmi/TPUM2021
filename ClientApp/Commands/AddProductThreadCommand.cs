using Client.LogicAPI;
using Client.LogicAPI.DTOs;
using Client.LogicAPI.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.App.Commands
{
    class AddProductThreadCommand : ICommand
    {
        public event Action OnExecute;

        private static int index = 0;
        private IEvidenceEntryService _productService = Logic.CreateEvidenceEntryService();
        private IConnectionService _connectionService = Logic.CreateConnectionService();
        private Thread _thread;
        private Random _random = new Random();

        public event EventHandler CanExecuteChanged;
        public bool IsActive { get; set; } = false;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            Task.Run(() => _connectionService.SendTask("send#client#all"));

        //    if (_thread == null)
        //    {
        //        _thread = new Thread(AddProductsLoop);
        //        _thread.Start();
        //    }

        //    IsActive = !IsActive;
        //    OnExecute?.Invoke();
        }

        private ProductDTO CreateDTO() => new ProductDTO() { Price = 2.0M, Name = $"New product {index++}" };

        private void AddProductsLoop()
        {
            while (true)
            {
                while (!IsActive) { }

                _productService.AddEvidenceEntryDTO(new EvidenceEntryDTO() 
                { 
                    Product = CreateDTO(),
                    ProductAmount = _random.Next(1,10),
                });
                Thread.Sleep(5000);
            }
        }
    }
}
