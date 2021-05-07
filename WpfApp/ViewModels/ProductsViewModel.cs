﻿using Server.LogicAPI;
using Server.LogicAPI.DTOs;
using Server.LogicAPI.Interfaces;
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
            Logic.EvidenceEntryChanged += OnEvidenceEntriessChanged;
        }

        ~ProductsViewModel()
        {
            Logic.EvidenceEntryChanged -= OnEvidenceEntriessChanged;
        }

        public ObservableCollection<EvidenceEntryDTO> Entries => new ObservableCollection<EvidenceEntryDTO>(_evidenceEntryService.GetAllEvidenceEntryDTOs());

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnEvidenceEntriessChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Entries"));
    }
}
