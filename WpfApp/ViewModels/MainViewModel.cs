using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace WpfApp.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            InputVM = new InputViewModel(Collection);
            ButtonTest = new ButtonTestCommand(Collection);
        }

        public InputViewModel InputVM { get; set; } // initialized in constructor
        public ICommand AddNewVM => InputVM.TryAdd;
        public ObservableCollection<ExampleViewModel> Collection { get; set; } = ExampleViewModel.Col();
        public ButtonTestCommand ButtonTest { get; private set; }
    }
}
