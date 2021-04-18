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
    class AddOrderThreadCommand : ICommand
    {
        private static int index = 0;
        private IOrderService _orderService = Logic.CreateOrderService();
        private IEvidenceEntryService _evidenceEntryService = Logic.CreateEvidenceEntryService();
        private IClientService _clientService = Logic.CreateClientService();
        private Thread _thread;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (_thread == null)
            {
                _thread = new Thread(AddOrdersLoop);
                _thread.Start();
            }
            else
            {
                _thread.Abort();
                _thread = null;
            }
        }

        private EvidenceEntryDTO CreateEvidenceEntryDTO() => _evidenceEntryService.GetEvidenceEntryDTOByID(0);
        private OrderDTO CreateDTO() => new OrderDTO() {  Products = new List<EvidenceEntryDTO>() { CreateEvidenceEntryDTO() }, Client = _clientService.GetClientDTOByID(0) };

        private void AddOrdersLoop()
        {
            while (true)
            {
                _orderService.AddOrderDTO(CreateDTO());
                Thread.Sleep(30000);
            }
        }
    }
}
