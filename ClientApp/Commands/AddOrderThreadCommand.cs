using Client.LogicAPI;
using Client.LogicAPI.DTOs;
using Client.LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.App.Commands
{
    class AddOrderThreadCommand : ICommand
    {
        public event Action OnExecute;

        private IOrderService _orderService = Logic.CreateOrderService();
        private IEvidenceEntryService _evidenceEntryService = Logic.CreateEvidenceEntryService();
        private IClientService _clientService = Logic.CreateClientService();
        private Thread _thread;

        public event EventHandler CanExecuteChanged;
        private IConnectionService _connectionService = Logic.CreateConnectionService();

        public bool IsActive { get; set; } = false;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            Task.Run(() => _connectionService.SendTask("add#product#{\"ID\":1,\"Name\":\"Cement (10kg)\",\"Price\":123.45}"));
            //if (_thread == null)
            //{
            //    _thread = new Thread(AddOrdersLoop);
            //    _thread.Start();
            //}

            //IsActive = !IsActive;
            //OnExecute?.Invoke();
        }

        private EvidenceEntryDTO CreateEvidenceEntryDTO() => _evidenceEntryService.GetEvidenceEntryDTOByID(0);
        private OrderDTO CreateDTO() => new OrderDTO() {  Products = new List<EvidenceEntryDTO>() { CreateEvidenceEntryDTO() }, Client = _clientService.GetClientDTOByID(0) };

        private void AddOrdersLoop()
        {
            while (true)
            {
                while (!IsActive) { }

                _orderService.AddOrderDTO(CreateDTO());
                Thread.Sleep(5000);
            }
        }
    }
}
