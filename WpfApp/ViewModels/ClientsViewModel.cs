using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WpfApp.ViewModels
{
    class ClientsViewModel : INotifyPropertyChanged
    {
        private string _test = "Clients";
        public string TestField
        {
            get => _test;
            set
            {
                _test = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TestField"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
