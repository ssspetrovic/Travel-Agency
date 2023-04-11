using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    internal class SingleReservationViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;
        private AccommodationDTO _accommodation;
        private ReservationDTO _reservation;
        BitmapImage _accommodationImage;
        private int _gradeClean;
        private int _gradeOwner;

        private readonly ReservationService reservationService = new();

        
        public BitmapImage Image
        {
            get => _accommodationImage;
            set
            {
                _accommodationImage = value;
            }
        }

        public AccommodationDTO Accommodation
        {
            get => _accommodation;
            set
            {
                _accommodation = value;
            }
        }

        public ReservationDTO Reservation
        {
            get => _reservation;
            set
            {
                _reservation = value;
                Accommodation = reservationService.GetAccommodation(_reservation.AccommodationId);
                Image = new BitmapImage(new Uri(Accommodation.PictureUrl, UriKind.Absolute));
            }
        }

        public SingleReservationViewModel()
        {
            var _reservationService = new ReservationService();
        }


        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
