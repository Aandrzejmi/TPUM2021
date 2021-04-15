using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public ExampleViewModel person = new ExampleViewModel { Name = "Salman", Age = 26 };
    }

    public class ExampleViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private static ICommand globalCommand = new GlobalCommand();

        public ICommand GlobalCommand => globalCommand;

        private ICommand command;

        public ICommand ShowName 
        {
            get
            {
                if (command == null)
                    command = new ExampleCommandHandler(this);

                return command;
            }
        }

        private string nameValue;

        public string Name
        {
            get => nameValue;
            set 
            {
                nameValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            } 
        }

        private double ageValue;

        public double Age
        {
            get => ageValue;
            set 
            {
                ageValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }
    }

    public class ExampleCommandHandler : ICommand
    {
        public ExampleCommandHandler(ExampleViewModel viewModel)
        {
            vm = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return vm.Age >= 18;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show($"My name is {vm.Name}");
        }

        private ExampleViewModel vm;
    }

    public class GlobalCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (parameter is Button b)
            {
                char nr = b.Name[^1];

                var window = Application.Current.MainWindow as MainWindow;
                window.lastButtonLabel.Content = $"Last button: {nr}";

                Thread t = new Thread(() => {
                    Thread.Sleep(3000);
                    window.person.Name = "Maria";
                });
                t.Start();
            }
        }
    }
}
