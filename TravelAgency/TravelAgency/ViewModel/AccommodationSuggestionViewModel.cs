using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TravelAgency.Command;
using TravelAgency.DTO;
using TravelAgency.Service;
using TravelAgency.View.Controls.Owner;
using TravelAgency.View;
using System.Windows;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel
{
    public class AccommodationSuggestionViewModel : BaseViewModel
    {
        private string _mostResAccName;
        private string _mostResAccLocation;
        private string _mostResAccNumOfRes;
        private string _mostResAccOccupiedDays;

        private string _mostOccupiedAccName;
        private string _mostOccupiedAccLocation;
        private string _mostOccupiedAccNumOfRes;
        private string _mostOccupiedAccOccupiedDays;

        private string _leastResAccName;
        private string _leastResAccLocation;
        private string _leastResAccNumOfRes;
        private string _leastResAccOccupiedDays;

        private string _leastOccupiedAccName;
        private string _leastOccupiedAccLocation;
        private string _leastOccupiedAccNumOfRes;
        private string _leastOccupiedAccOccupiedDays;

        public RelayCommand BtnRegister1 { get; set; }
        public RelayCommand BtnRegister2 { get; set; }
        public RelayCommand BtnDelete1 { get; set; }
        public RelayCommand BtnDelete2 { get; set; }

        public AccommodationSuggestionViewModel() 
        {
            SetValues();
            BtnRegister1 = new RelayCommand(Execute_BtnRegister1);
            BtnRegister2 = new RelayCommand(Execute_BtnRegister2);
            BtnDelete1 = new RelayCommand(Execute_BtnDelete1);
            BtnDelete2 = new RelayCommand(Execute_BtnDelete2);
        }
        private void Execute_BtnRegister1(object parameter)
        {
            OwnerMainView mainWindow = null;
            foreach (Window window in Application.Current.Windows)
            {
                if (window is OwnerMainView)
                {
                    mainWindow = (OwnerMainView)window;
                    break;
                }
            }

            Frame mainFrame = mainWindow.mainFrame;
            AccommodationService accommodationService = new AccommodationService();
            AccommodationSuggestionDTO acc = accommodationService.GetMostReservationsAcc();
            RegisterAccommodationView registerAccommodationView = new RegisterAccommodationView(acc.Location.City);
            mainFrame.Navigate(registerAccommodationView);
        }
        private void Execute_BtnRegister2(object parameter)
        {
            OwnerMainView mainWindow = null;
            foreach (Window window in Application.Current.Windows)
            {
                if (window is OwnerMainView)
                {
                    mainWindow = (OwnerMainView)window;
                    break;
                }
            }

            Frame mainFrame = mainWindow.mainFrame;
            AccommodationService accommodationService = new AccommodationService();
            AccommodationSuggestionDTO acc = accommodationService.GetMostOccupiedAcc();
            RegisterAccommodationView registerAccommodationView = new RegisterAccommodationView(acc.Location.City);
            mainFrame.Navigate(registerAccommodationView);
        }
        private void Execute_BtnDelete1(object parameter)
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            accommodationRepository.RemoveByName(LeastResAccName);
            Refresh();
        }
        private void Execute_BtnDelete2(object parameter)
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            accommodationRepository.RemoveByName(LeastOccupiedAccName);
            Refresh();
        }

        public string MostResAccName
        {
            get => _mostResAccName;

            set
            {
                _mostResAccName = value;
                OnPropertyChanged();
            }
        }
        public string MostResAccLocation
        {
            get => _mostResAccLocation;

            set
            {
                _mostResAccLocation = value;
                OnPropertyChanged();
            }
        }
        public string MostResAccNumOfRes
        {
            get => _mostResAccNumOfRes;

            set
            {
                _mostResAccNumOfRes = value;
                OnPropertyChanged();
            }
        }
        public string MostResAccOccupiedDays
        {
            get => _mostResAccOccupiedDays;

            set
            {
                _mostResAccOccupiedDays = value;
                OnPropertyChanged();
            }
        }

        public string MostOccupiedAccName
        {
            get => _mostOccupiedAccName;

            set
            {
                _mostOccupiedAccName = value;
                OnPropertyChanged();
            }
        }
        public string MostOccupiedAccLocation
        {
            get => _mostOccupiedAccLocation;

            set
            {
                _mostOccupiedAccLocation = value;
                OnPropertyChanged();
            }
        }
        public string MostOccupiedAccNumOfRes
        {
            get => _mostOccupiedAccNumOfRes;

            set
            {
                _mostOccupiedAccNumOfRes = value;
                OnPropertyChanged();
            }
        }
        public string MostOccupiedAccOccupiedDays
        {
            get => _mostOccupiedAccOccupiedDays;

            set
            {
                _mostOccupiedAccOccupiedDays = value;
                OnPropertyChanged();
            }
        }

        public string LeastResAccName
        {
            get => _leastResAccName;

            set
            {
                _leastResAccName = value;
                OnPropertyChanged();
            }
        }
        public string LeastResAccLocation
        {
            get => _leastResAccLocation;

            set
            {
                _leastResAccLocation = value;
                OnPropertyChanged();
            }
        }
        public string LeastResAccNumOfRes
        {
            get => _leastResAccNumOfRes;

            set
            {
                _leastResAccNumOfRes = value;
                OnPropertyChanged();
            }
        }
        public string LeastResAccOccupiedDays
        {
            get => _leastResAccOccupiedDays;

            set
            {
                _leastResAccOccupiedDays = value;
                OnPropertyChanged();
            }
        }

        public string LeastOccupiedAccName
        {
            get => _leastOccupiedAccName;

            set
            {
                _leastOccupiedAccName = value;
                OnPropertyChanged();
            }
        }
        public string LeastOccupiedAccLocation
        {
            get => _leastOccupiedAccLocation;

            set
            {
                _leastOccupiedAccLocation = value;
                OnPropertyChanged();
            }
        }
        public string LeastOccupiedAccNumOfRes
        {
            get => _leastOccupiedAccNumOfRes;

            set
            {
                _leastOccupiedAccNumOfRes = value;
                OnPropertyChanged();
            }
        }
        public string LeastOccupiedAccOccupiedDays
        {
            get => _leastOccupiedAccOccupiedDays;

            set
            {
                _leastOccupiedAccOccupiedDays = value;
                OnPropertyChanged();
            }
        }

        private void SetValues()
        {
            AccommodationService accommodationService = new AccommodationService();
            AccommodationSuggestionDTO acc = new AccommodationSuggestionDTO();

            //Most reservations
            acc = accommodationService.GetMostReservationsAcc();
            MostResAccName = "Name: " + acc.Name;
            MostResAccLocation = "Location: " + acc.Location.Country + ", " + acc.Location.City;
            MostResAccNumOfRes = "Number of reservations: " + acc.NumOfReseservations.ToString();
            MostResAccOccupiedDays = "Number of occupied days: " + acc.NumOfOccupiedDays.ToString();

            //Most occupied
            acc = accommodationService.GetMostOccupiedAcc();
            MostOccupiedAccName = "Name: " + acc.Name;
            MostOccupiedAccLocation = "Location: " + acc.Location.Country + ", " + acc.Location.City;
            MostOccupiedAccNumOfRes = "Number of reservations: " + acc.NumOfReseservations.ToString();
            MostOccupiedAccOccupiedDays = "Number of occupied days: " + acc.NumOfOccupiedDays.ToString();

            //Least reservations
            acc = accommodationService.GetLeastReservationsAcc();
            LeastResAccName = "Name: " + acc.Name;
            LeastResAccLocation = "Location: " + acc.Location.Country + ", " + acc.Location.City;
            LeastResAccNumOfRes = "Number of reservations: " + acc.NumOfReseservations.ToString();
            LeastResAccOccupiedDays = "Number of occupied days: " + acc.NumOfOccupiedDays.ToString();

            //Least occupied
            acc = accommodationService.GetLeastOccupiedAcc();
            LeastOccupiedAccName = "Name: " + acc.Name;
            LeastOccupiedAccLocation = "Location: " + acc.Location.Country + ", " + acc.Location.City;
            LeastOccupiedAccNumOfRes = "Number of reservations: " + acc.NumOfReseservations.ToString();
            LeastOccupiedAccOccupiedDays = "Number of occupied days: " + acc.NumOfOccupiedDays.ToString();
        }

        private void Refresh()
        {
            OwnerMainView mainWindow = null;
            foreach (Window window in Application.Current.Windows)
            {
                if (window is OwnerMainView)
                {
                    mainWindow = (OwnerMainView)window;
                    break;
                }
            }

            Frame mainFrame = mainWindow.mainFrame;
            AccommodationSuggestionView accommodationSuggestionView = new AccommodationSuggestionView();
            mainFrame.Navigate(accommodationSuggestionView);
        }
    }
}
