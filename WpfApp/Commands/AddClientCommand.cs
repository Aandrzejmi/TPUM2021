using LogicAPI;
using LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfApp.ViewModels;

namespace WpfApp.Commands
{
    class AddClientCommand : ICommand
    {
        private readonly NewClientViewModel _vm;
        private readonly IClientService _service;

        public AddClientCommand(NewClientViewModel vm)
        {
            _vm = vm;
            _service = Logic.CreateClientService();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _vm.Name != NewClientViewModel.defaultName
                && _vm.Adress != NewClientViewModel.defaultAddres
                && _vm.Name.Length > 1
                && _vm.Adress.Length > 1;
        }

        public void Execute(object parameter)
        {
            _service.AddClientDTO(_vm.CreateDTO());
            _vm.ResetFields();
        }
    }
}
