using LogicAPI;
using LogicAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WpfApp.Commands;

namespace WpfApp.ViewModels
{
    public class TempViewModel : INotifyPropertyChanged
    {
        public static string defaultName = "Entern new name";

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddNewVM { get; set; }
        public ICommand ButtonTest { get; private set; }

        public ObservableCollection<TempDTO> Collection { get; set; }

        public string NewName
        {
            get => _newName;
            set
            {
                _newName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NewName"));
            }
        }

        public int NewAge
        {
            get => _newAge;
            set
            {
                _newAge = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NewAge"));
            }
        }


        public TempViewModel()
        {
            Collection = new ObservableCollection<TempDTO>();
            Collection.Add(new TempDTO() { Name = "Anna Nowak", Age = 15 });
            Collection.Add(new TempDTO() { Name = "Jan Kowalski", Age = 34 });
            Collection.Add(new TempDTO() { Name = "Zygmunt Szmidt", Age = 66 });

            ButtonTest = new ButtonTestCommand(Collection);
            AddNewVM = new AddNewViewModellCommand(this);
        }

        private string _newName = defaultName;
        private int _newAge;
    }
}
