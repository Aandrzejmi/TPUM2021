using LogicAPI;
using LogicAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfApp.ViewModels;

namespace WpfApp.Commands
{
    public class AddNewViewModellCommand : ICommand
    {
        private readonly MainViewModel vm;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AddNewViewModellCommand(MainViewModel vm) => this.vm = vm;

        public bool CanExecute(object parameter)
        {
            return !vm.NewName.Equals(MainViewModel.defaultName)
                && vm.NewName.Length > 1
                && vm.NewAge > 0;
        }

        public void Execute(object parameter)
        {
            vm.Collection.Add(new ExampleDTO() { Name = vm.NewName, Age = vm.NewAge });
            vm.NewName = MainViewModel.defaultName;
            vm.NewAge = default;
        }
    }
}
