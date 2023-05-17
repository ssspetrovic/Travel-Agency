using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel
{
    internal class ProfileViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;
        private string _isSuperGuest;
        private DateTime _experationDate;
        private string _numberOfPoints;
        private string _buttonMessage;

        public ProfileViewModel() {
            CheckUserSuperGuest();
        }

        public string ButtonMessage
        {
            get => _buttonMessage;
            set
            {
                _buttonMessage = value;
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

        public DateTime ExperationDate
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

        public void CheckUserSuperGuest()
        {
            var _repository = new GuestRepository();
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
            RaisePropertyChanged("ButtonMessage");
            RaisePropertyChanged("IsSuperGuest");
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
