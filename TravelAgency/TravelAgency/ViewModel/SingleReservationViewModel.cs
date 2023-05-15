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
        private string _areGradesShown;
        private string _isTextShown;
        private string _gradeMessage;


        private readonly ReservationService reservationService = new();



        public string GradeMessage
        {
            get => _gradeMessage;
            set
            {
                _gradeMessage = value;
                OnPropertyChanged();
            }
        }
        public string AreGradesShown
        {
            get => _areGradesShown;
            set
            {
                _areGradesShown = value;
                OnPropertyChanged();
            }
        }

        public string IsTextShown
        {
            get => _isTextShown;
            set
            {
                _isTextShown = value;
                OnPropertyChanged();
            }
        }

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
                Image = new BitmapImage(new Uri(Accommodation.PictureUrl, UriKind.Absolute)); //TODO: change up
                StartDate = _reservation.StartDate.ToDateTime(TimeOnly.MinValue);
                EndDate = _reservation.EndDate.ToDateTime(TimeOnly.MinValue);
                UpdatePersonalGradeView();
            }
        }

        private Boolean ValidateSelect(ReservationDTO reservationDTO)
        {
            try
            {
                if (reservationDTO.Equals(null)) return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public SingleReservationViewModel()
        {
            GradeClean = 5;
            GradeOwner = 5;
            var _reservationService = new ReservationService();
        }

        public void UpdatePersonalGradeView()
        {
            var _service = new ReservationService();
            
            if (_reservation == null)
            {
                MessageBox.Show("There was a problem while loading the reservation in question.");
            }
            else if(Reservation.GradeAccommodationClean == -1 || Reservation.GradeAccommodationOwner == -1)
            {
                AreGradesShown = "Hidden";
                IsTextShown = "Visible";
                GradeMessage = "You need to rate the owner before you can see your rating";
                RaisePropertyChanged("AreGradesShown");
                RaisePropertyChanged("IsTextShown");
                RaisePropertyChanged("GradeMessage");


            }
            else if(Reservation.GradeGuestClean == -1 || Reservation.GradeGuestComplacent == -1)
            {
                AreGradesShown = "Hidden";
                IsTextShown = "Visible";
                GradeMessage = "You have not been rated";
                RaisePropertyChanged("AreGradesShown");
                RaisePropertyChanged("IsTextShown");
                RaisePropertyChanged("GradeMessage");
            }
            else
            {
                AreGradesShown = "Visible";
                IsTextShown = "Hidden";
                RaisePropertyChanged("AreGradesShown");
                RaisePropertyChanged("IsTextShown");
            }
            
        }

        public void SendDelayRequest()
        {
            var _service = new DelayRequestService();
            var OldStartDate = _reservation.StartDate.ToDateTime(TimeOnly.MinValue);
            var OldEndDate = _reservation.EndDate.ToDateTime(TimeOnly.MinValue);
            var delayRequest = new DelayRequest(Reservation.Id, Accommodation.Id, CurrentUser.Id, OldStartDate, StartDate, OldEndDate, EndDate, RequestStatusType.Processing, "");
            _service.Add(delayRequest);
        }

        private Boolean ValidateSelect(AccommodationDTO accommodationDTO)
        {
            try
            {
                if (accommodationDTO.Equals(null)) return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        private Boolean ValidateNumberInput(string str)
        {
            int temp;
            return int.TryParse(str, out int n);
        }
        private Boolean ValidateStringInput(string str)
        {
            if (str == string.Empty) return false;
            return true;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public void SubmitRating()
        {
            if(!ValidateStringInput(Comment) && !ValidateStringInput(PictureUrl))
            {
                MessageBox.Show("Not all fields have been filled!");
            }
            else if((DateOnly.FromDateTime(DateTime.Now).DayNumber - Reservation.StartDate.DayNumber) <= 5)
            {
                var _service = new ReservationService();
                _service.GradeAccommodation(Reservation.Id, Comment, PictureUrl, GradeClean, GradeOwner);
                MessageBox.Show("Rating Submitted!");
            }
            else
            {
                MessageBox.Show("You need to submit ratings within 5 days!");
            }

            UpdatePersonalGradeView();
        }
    }
}
