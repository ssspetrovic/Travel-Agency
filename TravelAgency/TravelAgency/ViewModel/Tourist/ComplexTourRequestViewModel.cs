using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Service;
using static System.Windows.Application;
using TravelAgency.View.Tourist;
using System.Linq;
using System.Windows.Input;

namespace TravelAgency.ViewModel.Tourist
{
    internal class ComplexTourRequestViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly TouristViewModel _touristViewModel;

        private ObservableCollection<RequestTour> _tourParts;
        private Language? _language;
        private string? _guestNumber;
        private DateTime? _startingDate;
        private DateTime? _endingDate;
        private string? _description;
        private Array _languages;
        private ObservableCollection<Location> _locationsCollection;
        private Location? _selectedLocation;
        private OkDialog? Dialog { get; set; }

        public ObservableCollection<RequestTour> TourParts
        {
            get => _tourParts;
            set
            {
                _tourParts = value;
                OnPropertyChanged();
            }
        }

        public Location? SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Location> LocationsCollection
        {
            get => _locationsCollection;
            set
            {
                _locationsCollection = value;
                OnPropertyChanged();
            }
        }

        public Language? Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChanged();
            }
        }

        public string? GuestNumber
        {
            get => _guestNumber;
            set
            {
                _guestNumber = value;
                OnPropertyChanged();
            }
        }

        public DateTime? StartingDate
        {
            get => _startingDate;
            set
            {
                _startingDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? EndingDate
        {
            get => _endingDate;
            set
            {
                _endingDate = value;
                OnPropertyChanged();
            }
        }

        public string? Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public Array Languages
        {
            get => _languages;
            set
            {
                _languages = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand AddTourPartCommand { get; set; }
        public RelayCommand RemoveTourPartCommand { get; set; }

        public ComplexTourRequestViewModel(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            _navigationService = navigationService;
            _touristViewModel = touristViewModel;

            _tourParts = new ObservableCollection<RequestTour>();

            _languages = Enum.GetValues(typeof(Language));
            var locationService = new LocationService();
            _locationsCollection = locationService.GetAll();

            CancelCommand = new RelayCommand(Execute_CancelCommand);
            SubmitCommand = new RelayCommand(Execute_SubmitCommand, CanExecute_SubmitCommand);
            AddTourPartCommand = new RelayCommand(Execute_AddTourPartCommand, CanExecute_AddTourPartCommand);
            RemoveTourPartCommand = new RelayCommand(Execute_RemoveTourPartCommand);
        }

        private bool IsDateRangeValid(DateTime? startingDate, DateTime? endingDate)
        {
            if (startingDate < DateTime.Today || startingDate == null || endingDate == null) return false;
            return startingDate < endingDate;
        }

        private bool IsDuplicateTourPart(Location? location)
        {
            return TourParts.Any(t => t.Location.Equals(location));
        }

        private void PopDialog(string text, ICommand command)
        {
            Dialog = new OkDialog
            {
                Owner = Current.MainWindow,
                TextBlock =
                {
                    Text = text
                },
                Button =
                {
                    Command = command
                }
            };

            Dialog.ShowDialog();
        }

        private void AddTourPart()
        {
            var formattedDateRange =
                $"{StartingDate?.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)} - {EndingDate?.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}";

            TourParts.Add(
                new RequestTour(
                    SelectedLocation!,
                    Description!, 
                    Language!,
                    int.Parse(GuestNumber!),
                    formattedDateRange,
                    Status.OnHold,
                    CurrentUser.Username,
                    true
                    )
                );
        }

        private void HandleTourPartAddition()
        {
            if (IsDuplicateTourPart(SelectedLocation))
            {
                const string text = "You already have a tour part with the same location.";
                var command = new RelayCommand(Execute_CloseDialogCommand);
                PopDialog(text, command);
            }
            else
            {
                AddTourPart();
            }
        }

        private void Execute_CancelCommand(object parameter)
        {
            _navigationService.GoBack();
        }

        private void Execute_NavigateToMyRequestsCommand(object parameter)
        {
            Dialog!.Close();
            _navigationService.Navigate(new MyRequestsView(_navigationService, _touristViewModel));
        }

        private void Execute_SubmitCommand(object parameter)
        {
            var complexTourRequestService = new ComplexTourRequestService();
            complexTourRequestService.HandleComplexRequest(TourParts);

            const string text = "Complex tour successfully created!";
            var command = new RelayCommand(Execute_NavigateToMyRequestsCommand);
            PopDialog(text, command);
        }

        private bool CanExecute_SubmitCommand(object parameter)
        {
            return TourParts.Count > 1;
        }

        private void Execute_CloseDialogCommand(object parameter)
        {
            Dialog!.Close();
        }

        private void Execute_AddTourPartCommand(object parameter)
        {
            HandleTourPartAddition();
        }

        private bool CanExecute_AddTourPartCommand(object parameter)
        {
            return SelectedLocation != null &&
                   Language != null &&
                   IsDateRangeValid(StartingDate, EndingDate) &&
                   int.TryParse(GuestNumber, out _) &&
                   !string.IsNullOrEmpty(Description);
        }

        private void Execute_RemoveTourPartCommand(object parameter)
        {
            if (parameter is not RequestTour tourPart) return;
            TourParts.Remove(tourPart);
            CollectionViewSource.GetDefaultView(TourParts).Refresh();
        }
    }
}
