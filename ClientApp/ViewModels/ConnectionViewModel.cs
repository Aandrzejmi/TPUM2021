using Client.App.Commands;
using Client.LogicAPI;
using Client.LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.App.ViewModels
{
    class ConnectionViewModel : INotifyPropertyChanged
    {
        public bool _connected;
        public ConnectionCommand ConnectButtonCommand { get; set; }

        public ConnectionViewModel()
        {
            _connected = false;
            ConnectButtonCommand = new ConnectionCommand(this);
        }

        public bool Connected
        {
            get => _connected;
            set
            {
                _connected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Connected"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ButtonLabel"));
            }
        }

        public string ButtonLabel => _connected ? "Disconnect" : "Connect";

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
