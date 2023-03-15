using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.Model;

namespace TravelAgency.ViewModel
{
    internal class TourReservationListingViewModel : BaseViewModel
    {
        private readonly ObservableCollection<TourModel> _tours;

        public ICommand MakeTourReservationCommand { get; }

        public TourReservationListingViewModel()
        {
            
        }
    }
}
