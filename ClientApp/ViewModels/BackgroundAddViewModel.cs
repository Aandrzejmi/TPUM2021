using System.ComponentModel;
using Client.App.Commands;

namespace Client.App.ViewModels
{
    class BackgroundAddViewModel : INotifyPropertyChanged
    {
        public RefreshDataCommand Refresh { get; set; } = new RefreshDataCommand();
        public CloseCommand Disconnect { get; set; } = new CloseCommand();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
