using LogicAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace WpfApp.ViewModels
{
    class ProductsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<EvidenceEntryDTO> Entries { get; set; } = new ObservableCollection<EvidenceEntryDTO>()
        {
            new EvidenceEntryDTO(){ Product = new ProductDTO() {ID = 1, Name = "Kawa", Price = 5.20M }, ProductAmount = 5 },
            new EvidenceEntryDTO(){ Product = new ProductDTO() {ID = 2, Name = "Ładowarka USB C", Price = 15.50M }, ProductAmount = 3},
            new EvidenceEntryDTO(){ Product = new ProductDTO() {ID = 3, Name = "Lampka nocna", Price = 65.99M }, ProductAmount = 1},
        };

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
