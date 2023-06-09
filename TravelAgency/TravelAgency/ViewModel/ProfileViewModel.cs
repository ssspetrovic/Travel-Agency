using SQLitePCL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    internal class ProfileViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;
        private string _isSuperGuest;
        private string _experationDate;
        private string _numberOfPoints;
        private string _buttonMessage;
        private string phoneNumber;
        private string userName;
        private string email;
        private string firstName;
        private string lastName;



        public ProfileViewModel() {
            InitSuperGuest();
            phoneNumber = "+381 63-198-745-0";
            email = "dragana@gmail.com";
            userName = "DraganaDraginic";
            firstName = "Dragana";
            lastName = "Draginic";
            var _service = new GuestService();
            var _repository = new GuestRepository();
            Guest guest = _repository.GetByUserId(CurrentUser.Id);
            _service.UpdateState(guest);
        }

        public string ButtonMessage
        {
            get => _buttonMessage;
            set
            {
                _buttonMessage = value;
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
            }
        }

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
            }
        }

        public string NumberOfPoints
        {
            get => _numberOfPoints;
            set
            {
                _numberOfPoints = value;
                OnPropertyChanged();
            }
        }

        public string ExperationDate
        {
            get => _experationDate;
            set
            {
                _experationDate = value;
            }
        }
        public string IsSuperGuest
        {
            get => _isSuperGuest;
            set
            {
                _isSuperGuest = value;
                OnPropertyChanged();
            }
        }

        public void InitSuperGuest()
        {
            var _repository = new GuestRepository();
            Guest guest = _repository.GetByUserId(CurrentUser.Id);

            if(_repository.IsSuperGuest(CurrentUser.Id))
            {
                ButtonMessage = "Renew super guest status";
                IsSuperGuest = "Visible";
            }
            else
            {
                ButtonMessage = "Request super guest status";
                IsSuperGuest = "Hidden";
            }
            NumberOfPoints = _repository.GetByUserId(CurrentUser.Id).Credits.ToString();
            DateTime date = guest.SuperGuestExpDate ?? DateTime.Now;
            ExperationDate = date.ToString("yyyy-dd-MM");
            RaisePropertyChanged("ButtonMessage");
            RaisePropertyChanged("IsSuperGuest");
            RaisePropertyChanged("NumberOfPoints");
            RaisePropertyChanged("ExperationDate");
        }

        public bool RequestSuperGuest()
        {
            var _repository = new ReservationRepository();
            var _guestRepository = new GuestRepository();
            DateTime limit = DateTime.Now.AddYears(-1);
            if(_repository.CountBeforeDate(limit) >= 10)
            {
                _guestRepository.MakeSuperGuest(CurrentUser.Id);
            }
            return false;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
