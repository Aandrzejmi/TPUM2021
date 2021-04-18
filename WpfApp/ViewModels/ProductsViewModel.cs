using LogicAPI;
using LogicAPI.DTOs;
using LogicAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace WpfApp.ViewModels
{
    class ProductsViewModel : INotifyPropertyChanged
    {
        private IEvidenceEntryService _evidenceEntryService;

        public ProductsViewModel()
        {
            _evidenceEntryService = Logic.CreateEvidenceEntryService();
            Logic.ProductsChanged += OnProductsChanged;
            Logic.EvidenceEntryChanged += OnProductsChanged;
        }

        ~ProductsViewModel()
        {
            Logic.ProductsChanged -= OnProductsChanged;
            Logic.EvidenceEntryChanged -= OnProductsChanged;
        }

        public ObservableCollection<EvidenceEntryDTO> Entries => new ObservableCollection<EvidenceEntryDTO>(_evidenceEntryService.GetAllEvidenceEntryDTOs());

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnProductsChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Entries"));
    }
}
