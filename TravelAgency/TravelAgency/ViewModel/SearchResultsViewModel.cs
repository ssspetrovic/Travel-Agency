﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using TravelAgency.DTO;
using TravelAgency.Repository;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    internal class SearchResultsViewModel: BaseViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;
        private CollectionViewSource _collection;


        public ICollectionView Collection => _collection.View;

        public SearchResultsViewModel()
        {
            var _accommodationService = new AccommodationService();
            _collection = new CollectionViewSource
            {
                Source = _accommodationService.GetAllForSearchResult()
            };
        }

        public CollectionViewSource CollectionView
        {
            get => _collection;
            set
            {
                _collection = value;
                OnPropertyChanged();
            }
        }



        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
