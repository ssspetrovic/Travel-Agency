using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.View.Controls.Tourist;
using static System.Windows.Application;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace TravelAgency.ViewModel
{
    public class TourReservationViewModel : BaseViewModel
    {
        private string? _filterText;
        private readonly CollectionViewSource _toursCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private bool _isFilteredCollectionEmpty;
        private bool _isListViewShown;
        private bool _shouldUpdateFilteredCollectionEmpty;
        private TourModel? _selectedTour;
        private string? _guestNumber;
        private bool _isTourSelected;
        private bool _isGuestNumberEntered;
        private string _newGuestNumber;
        private string _guestNumberText;

        private readonly TourRepository _tourRepository;
        private readonly TourReservationRepository _reservationRepository;

        private TourReservationView _mainWindow;

        public string NewGuestNumber
        {
            get => _newGuestNumber;
            set
            {
                _newGuestNumber = value;
                OnPropertyChanged();
            }
        }

        public string GuestNumberText
        {
            get => _guestNumberText;
            set
            {
                _guestNumberText = value;
                OnPropertyChanged();
            }
        }

        public bool IsTourSelected
        {
            get => _isTourSelected;
            set
            {
                _isTourSelected = value;
                OnPropertyChanged();
            }
        }

        public string? GuestNumber
        {
            get => _guestNumber;
            set
            {
                _guestNumber = value;
                _shouldUpdateFilteredCollectionEmpty = true;
                _toursCollection.View.Refresh();
                RaisePropertyChanged("FilterText");
                OnPropertyChanged();
            }
        }

        public TourModel? SelectedTour
        {
            get => _selectedTour;

            set
            {
                _selectedTour = value;
                IsTourSelected = true;
                OnPropertyChanged();
                //Debug.WriteLine(_selectedTour.Name);
            }
        }

        public bool IsFilteredCollectionEmpty
        {
            get => _isFilteredCollectionEmpty;
            set
            {
                if (_isFilteredCollectionEmpty == value) return;

                _isFilteredCollectionEmpty = value;
                OnPropertyChanged();
            }
        }

        public bool IsListViewShown
        {
            get => _isListViewShown;
            set
            {
                if (_isListViewShown == value) return;

                _isListViewShown = value;
                OnPropertyChanged();
            }
        }

        public string? FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                _shouldUpdateFilteredCollectionEmpty = true;
                _toursCollection.View.Refresh();
                RaisePropertyChanged("FilterText");
            }
        }
        public ICollectionView ToursSourceCollection => _toursCollection.View;

        public TourReservationViewModel()
        {
            _tourRepository = new TourRepository();

            _toursCollection = new CollectionViewSource
            {
                Source = _tourRepository.GetAll()
            };
            _toursCollection.Filter += ToursCollection_Filter;

            if (!ToursSourceCollection.IsEmpty)
                IsListViewShown = true;

            IsTourSelected = false;
            _isGuestNumberEntered = false;
            _reservationRepository = new TourReservationRepository();
        }

        private bool DoGuestsFit(int maxGuests)
        {
            int.TryParse(GuestNumber, out var guestNumber);
            return guestNumber <= maxGuests;
        }

        private bool DoesLocationFit(LocationModel location)
        {
            return location.City.Equals(SelectedTour?.Location.City) &&
                   location.Country.Equals(SelectedTour?.Location.Country);
        }

        private void ToursCollection_Filter(object sender, FilterEventArgs e)
        {
            // Checks if "tour = e.Item as Tour" is true
            if (e.Item is not TourModel tour) return;

            if (string.IsNullOrEmpty(FilterText))
            {
                //Debug.WriteLine(tour.MaxGuests);
                e.Accepted = true;
                if (tour.MaxGuests <= 0)
                {
                    e.Accepted = false;
                    //Debug.WriteLine("in with: " + tour.MaxGuests);
                }
                return;
            }

            var filterTextUpper = FilterText.ToUpper();

            if (_isGuestNumberEntered)
            {
                //Debug.WriteLine("in");
                //Debug.WriteLine("selected")
                //Debug.WriteLine($"{tour.Name}: {tour.MaxGuests} - entered guests {guestNumber}");
                //Debug.WriteLine("Tour location: " + tour.Location);
                //Debug.WriteLine("Selected location: " + SelectedTour?.Location);
                //Debug.WriteLine(tour.Location.City.Equals(SelectedTour?.Location.City) && tour.Location.Country.Equals(SelectedTour?.Location.Country));

                if (DoGuestsFit(tour.MaxGuests) && DoesLocationFit(tour.Location) && tour.MaxGuests > 0)
                {
                    //Debug.WriteLine($"accepted: {tour.Name}");
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            else
            {
                if (tour.Location.City.ToUpper().Contains(filterTextUpper) || tour.Location.Country.ToUpper().Contains(filterTextUpper) ||
                    tour.Duration.ToString(CultureInfo.InvariantCulture).ToUpper().Contains(filterTextUpper) ||
                    tour.Language.ToString().ToUpper().Contains(filterTextUpper) || tour.MaxGuests.ToString().ToUpper().Contains(filterTextUpper) && tour.MaxGuests > 0)
                {
                    e.Accepted = true;
                }
                else
                {
                    //Debug.WriteLine($"declined: {tour.Name}");
                    e.Accepted = false;
                }
            }

            _shouldUpdateFilteredCollectionEmpty = true;
            IsFilteredCollectionEmpty = !e.Accepted && ToursSourceCollection.IsEmpty;
            IsListViewShown = !IsFilteredCollectionEmpty;
        }

        private void UpdateFilteredCollectionEmpty()
        {
            IsFilteredCollectionEmpty = ToursSourceCollection.IsEmpty;
            IsListViewShown = !IsFilteredCollectionEmpty;
            _shouldUpdateFilteredCollectionEmpty = false;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (_shouldUpdateFilteredCollectionEmpty)
            {
                UpdateFilteredCollectionEmpty();
            }
        }

        private void CompleteReservation(int guestNumber)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Failed to complete reservation!");
                return;
            }

            if (CurrentUser.Username == null || CurrentUser.DisplayName == null)
            {
                MessageBox.Show("Failed to fetch current user data!");
            }
            else
            {
                _reservationRepository.Add(new TourReservation(SelectedTour.Id, SelectedTour.Name, guestNumber, CurrentUser.Username, CurrentUser.DisplayName));
                MessageBox.Show("Reservation was successful!", "Tour reservation");
            }
        }

        public void MakeReservation()
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Error while selecting tour. Please try again.");
                return;
            }

            // SelectedTour contains the tour we selected in the moment of using button
            //Debug.WriteLine(SelectedTour);
            if (!int.TryParse(GuestNumber, out var guestNumber) || guestNumber <= 0)
            {
                MessageBox.Show("Invalid number of guests!");
                return;
            }

            if (guestNumber > SelectedTour.MaxGuests)
            {
                MessageBox.Show("No room for that amount on guests. Showing other available options on the same location.");
                _isGuestNumberEntered = true;
                _toursCollection.View.Refresh();
                FilterText = " ";
                return;
            }

            var finalGuestNumber = -1;

            if (guestNumber < SelectedTour.MaxGuests)
            {
                var dialog = new GuestNumberDialog
                {
                    Owner = Current.MainWindow,
                    DataContext = this
                };

                GuestNumberText =
                    $"Tour still isn't full. Number of spaces left: {SelectedTour.MaxGuests - guestNumber}";

                //Debug.WriteLine(GuestNumberDialog.IsUpdateConfirmed);
                dialog.ShowDialog();
                //Debug.WriteLine(GuestNumberDialog.IsUpdateConfirmed);

                // Number of guests wasn't changed or cancel was pressed
                if (NewGuestNumber == null || !GuestNumberDialog.IsUpdateConfirmed)
                {
                    //Debug.WriteLine("in old");
                    finalGuestNumber = guestNumber;
                }
                else
                {
                    if (!int.TryParse(NewGuestNumber, out var newGuestNumber) ||
                        newGuestNumber > SelectedTour.MaxGuests)
                    {
                        MessageBox.Show("Invalid number of guests!");
                    }
                    else
                    {
                        finalGuestNumber = newGuestNumber;
                    }
                }
            }
            else
            {
                // Case where the number of guest is exact as max number of guests
                finalGuestNumber = guestNumber;
            }

            if (finalGuestNumber == -1)
            {
                MessageBox.Show("Failed to make reservation!");
            }
            else
            {
                _tourRepository.UpdateMaxGuests(SelectedTour.Id, SelectedTour.MaxGuests - finalGuestNumber);
                CompleteReservation(finalGuestNumber);
                ReloadWindow();
            }

            FilterText = " ";
            IsTourSelected = false;
            _toursCollection.View.Refresh();
        }

        private void ReloadWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _mainWindow ??= new TourReservationView();
                var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                _mainWindow.Show();
                currentWindow?.Close();
            });
        }
    }
}
