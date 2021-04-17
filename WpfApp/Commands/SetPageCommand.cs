using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfApp.ViewModels;

namespace WpfApp.Commands
{
    class SetPageCommand : ICommand
    {
        public readonly string pagePath;
        private readonly NavigationViewModel navigation;

        public SetPageCommand(NavigationViewModel navi, string path)
        {
            navigation = navi;
            pagePath = path;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            navigation.CurrentPage = pagePath;
        }
    }
}
