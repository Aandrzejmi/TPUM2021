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
        private ObservableCollection<EvidenceEntryDTO> _entries;

        public ProductsViewModel()
        {
            _evidenceEntryService = Logic.CreateEvidenceEntryService();
        }

        public ObservableCollection<EvidenceEntryDTO> Entries
        {
            get
            {
                _entries = new ObservableCollection<EvidenceEntryDTO>(_evidenceEntryService.GetAllEvidenceEntryDTOs());
                return _entries;
            }
            set
            {
                _entries = value;
                // _clientService.???
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
