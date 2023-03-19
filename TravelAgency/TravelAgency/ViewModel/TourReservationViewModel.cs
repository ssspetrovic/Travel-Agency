using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
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
        private ObservableCollection<TourModel> _toursCollection;

        public TourReservationViewModel()
        {
            var tourRepository = new TourRepository();
            _toursCollection = tourRepository.GetAll();
        }

        public ObservableCollection<TourModel> ToursCollection
        {
            get => _toursCollection;
            set
            {
                _toursCollection = value;
                OnPropertyChanged();
            }
        }

        private void TourFilter(object sender, FilterEventArgs e)
        {
            
        }
    }
}
