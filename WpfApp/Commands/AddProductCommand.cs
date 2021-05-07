using Server.LogicAPI;
using Server.LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfApp.ViewModels;

namespace WpfApp.Commands
{
    class AddProductCommand : ICommand
    {
        private readonly NewProductViewModel _vm;
        private readonly IEvidenceEntryService _service;

        public AddProductCommand(NewProductViewModel vm)
        {
            _vm = vm;
            _service = Logic.CreateEvidenceEntryService();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _vm.Name != NewProductViewModel.defaultName
                && _vm.Name.Length > 1
                && _vm.Price > 0
                && _vm.Amount > 0;
        }

        public void Execute(object parameter)
        {
            _service.AddEvidenceEntryDTO(_vm.CreateDTO());
            _vm.ResetFields();
        }
    }
}
