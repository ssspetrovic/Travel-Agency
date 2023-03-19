using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel
{
    public class TourReservationViewModel : BaseViewModel
    {
        private string? _filterText;
        private readonly CollectionViewSource _toursCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;

        public string? FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                _toursCollection.View.Refresh();
                RaisePropertyChanged("FilterText");
            }
        }

        public TourReservationViewModel()
        {
            var tourRepository = new TourRepository();

            _toursCollection = new CollectionViewSource
            {
                Source = tourRepository.GetAll()
            };
            _toursCollection.Filter += ToursCollection_Filter;
        }

        public ICollectionView ToursSourceCollection => _toursCollection.View;

        private void ToursCollection_Filter(object sender, FilterEventArgs e)
        {

            Debug.WriteLine(FilterText);
            if (string.IsNullOrEmpty(FilterText))
            {
                Debug.WriteLine("Ne valja");
                e.Accepted = true;
                return;
            }

            // Checks if "tour = e.Item as Tour" is true
            if (e.Item is not TourModel tour) return;

            var filterTextUpper = FilterText.ToUpper();

            if (tour.Location.City.ToUpper().Contains(filterTextUpper) || tour.Location.Country.ToUpper().Contains(filterTextUpper) ||
                tour.Duration.ToString(CultureInfo.InvariantCulture).ToUpper().Contains(filterTextUpper) ||
                tour.Language.ToString().ToUpper().Contains(filterTextUpper) || tour.MaxGuests.ToString().ToUpper().Contains(filterTextUpper))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
            Debug.WriteLine(e.Accepted);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
