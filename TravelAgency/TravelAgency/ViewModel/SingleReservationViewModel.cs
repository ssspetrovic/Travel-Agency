using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private DateTime _startDate;
        private DateTime _endDate;
        private string _comment;
        private string _pictureUrl;


        private readonly ReservationService reservationService = new();


        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }

        public string PictureUrl
        {
            get => _pictureUrl;
            set
            {
                _pictureUrl = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
            }
        }
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
            }
        }
        public int GradeClean
        {
            get => _gradeClean;
            set
            {
                _gradeClean = value;
                OnPropertyChanged();
            }
        }

        public int GradeOwner
        {
            get => _gradeOwner;
            set
            {
                _gradeOwner = value;
                OnPropertyChanged();
            }
        }
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
                StartDate = _reservation.StartDate.ToDateTime(TimeOnly.MinValue);
                EndDate = _reservation.EndDate.ToDateTime(TimeOnly.MinValue);
            }
        }

        public SingleReservationViewModel()
        {
            GradeClean = 5;
            GradeOwner = 5;
            var _reservationService = new ReservationService();
        }

        public void SendDelayRequest()
        {
            var _service = new DelayRequestService();
            var OldStartDate = _reservation.StartDate.ToDateTime(TimeOnly.MinValue);
            var OldEndDate = _reservation.EndDate.ToDateTime(TimeOnly.MinValue);
            var delayRequest = new DelayRequest(Reservation.Id, Accommodation.Id, CurrentUser.Id, OldStartDate, StartDate, OldEndDate, EndDate, RequestStatusType.Processing, "");
            _service.Add(delayRequest);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public void SubmitRating()
        {
            if((DateOnly.FromDateTime(DateTime.Now).DayNumber - Reservation.StartDate.DayNumber) <= 5)
            {
                var _service = new ReservationService();
                _service.GradeAccommodation(Reservation.Id, Comment, PictureUrl, GradeClean, GradeOwner);
                MessageBox.Show("Rating Submitted!");
            }
            else
            {
                MessageBox.Show("You need to submit ratings within 5 days!");
            }
        }
    }
}
