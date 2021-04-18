using LogicAPI;
using LogicAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp.Commands
{
    public class ButtonTestCommand : ICommand
    {
        ObservableCollection<ExampleDTO> collection;

        public ButtonTestCommand(ObservableCollection<ExampleDTO> collection) => this.collection = collection;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (parameter is Button b)
            {
                char nr = b.Name[^1];

                var window = Application.Current.MainWindow as MainWindow;
                //window.lastButtonLabel.Content = $"Last button: {nr}";

                Thread t = new Thread(() => {
                    Thread.Sleep(3000);
                    collection[0].Name = "Maria";
                });
                t.Start();
            }
        }
    }
}
