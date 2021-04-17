using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public InputViewModel InputVM { get; set; } // initialized in constructor
        public ICommand AddNewVM => InputVM.TryAdd;
        public ObservableCollection<ExampleViewModel> Collection { get; set; } = ExampleViewModel.Col();
        public ButtonTestCommand ButtonTest { get; private set; } = new ButtonTestCommand();
    }

    public class ExampleViewModel : INotifyPropertyChanged, ICloneable
    {
        public static ObservableCollection<ExampleViewModel> Col()
        {
            var c = new ObservableCollection<ExampleViewModel>();
            c.Add(new ExampleViewModel() { Name = "Anna Nowak", Age = 15 });
            c.Add(new ExampleViewModel() { Name = "Jan Kowalski", Age = 34 });
            c.Add(new ExampleViewModel() { Name = "Zygmunt Szmidt", Age = 66 });
            return c;
        }

        public object Clone()
        {
            return new ExampleViewModel() { Name = nameValue, Age = ageValue };
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

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

    public class InputViewModel : INotifyPropertyChanged
    {
        public static readonly string defaultName = "Enter name";
        public static readonly string defaultAge = "Enter Age";

        private string name = defaultName;
        private string age = defaultAge;

        public ObservableCollection<ExampleViewModel> Collection { get; set; }
        public ICommand TryAdd { get; private set; }

        public InputViewModel(ObservableCollection<ExampleViewModel> target)
        {
            Collection = target;
            TryAdd = new AddNewViewModellCommand(this);
        }

        public string InputName
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InputName"));
            }
        }

        public string InputAge
        {
            get => age;
            set
            {
                age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InputAge"));
            }
        }

        public void Reset()
        {
            InputName = defaultName;
            InputAge = defaultAge;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


    public class ButtonTestCommand : ICommand
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
                    window.Collection[0].Name = "Maria";
                });
                t.Start();
            }
        }
    }

    public class AddNewViewModellCommand : ICommand
    {
        private int age;
        private InputViewModel input;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public AddNewViewModellCommand(InputViewModel vm) => input = vm;

        public bool CanExecute(object parameter)
        {
            bool noDefaultName = !input.InputName.Equals(InputViewModel.defaultName);
            bool nameLength = input.InputName.Length > 1;
            bool parseAge = int.TryParse(input.InputAge, out age);
            bool validAge = age > 0;

            return noDefaultName && nameLength && parseAge && validAge;
        }

        public void Execute(object parameter)
        {
            input.Collection.Add(new ExampleViewModel() { Name = input.InputName, Age = age });
            input.Reset();
        }
    }
}
