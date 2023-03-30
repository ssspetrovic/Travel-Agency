using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TravelAgency.Model;


namespace TravelAgency.ViewModel
{
    internal class ReservationViewModel: BaseViewModel, INotifyPropertyChanged
    {
        private readonly CollectionViewSource _reservationCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private Reservation _selectedReservation;
    }
}
