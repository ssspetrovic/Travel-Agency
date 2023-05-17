using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace TravelAgency.ViewModel
{
    public class CreateTourViewModel : HomePageViewModel
    {
        private string? _nameText;
        private string? _comboBoxLocation;
        private string? _descriptionText;
        private string? _comboBoxLanguage;
        private string? _maxGuestsText;
        private string? _keyPointsList;
        private string? _dateList;
        private DateTime? _datePick = DateTime.Today;
        private string? _durationText;
        private string? _imagesList;
        private string? _imagesText;
        private string? _errorMessageText;
        private string? _comboBoxKeyPoints;
        private bool _isCreateTourEnabled;

        private SolidColorBrush _locationBackground = Brushes.Transparent;
        private SolidColorBrush _languageBackground = Brushes.Transparent;
        private SolidColorBrush _guestsBackground = Brushes.Transparent;
        private SolidColorBrush _dateBackground = Brushes.Transparent;
        private SolidColorBrush _errorTextColor = Brushes.DarkRed;

        private bool _locationFocus = true;
        private bool _languageFocus = true;
        private bool _guestsFocus = true;
        private bool _dateFocus = true;

        private int _languageIndex;
        private int _locationIndex;

        private readonly TourService _tourService;
        private readonly LocationService _locationService;
        private readonly RequestTourService _requestTourService;

        public CreateTourViewModel()
        {
            _tourService = new TourService();
            _locationService = new LocationService();
            _requestTourService = new RequestTourService();
            CreateTourCommand = new MyICommand<string>(Confirm);
            KeyPointsCommands = new MyICommand<string>(KeyPoints);
            ImagesCommands = new MyICommand<string>(Images);

            _dateList = CreateAcceptedTourDto.DateList;
            _maxGuestsText = CreateAcceptedTourDto.MaxGuests;
            _comboBoxLanguage = CreateAcceptedTourDto.Language.Trim();
            _comboBoxLocation = CreateAcceptedTourDto.Location.Trim();

            if (_comboBoxLocation != string.Empty)
            {
                _locationIndex = _locationService.GetByCity(_comboBoxLocation)!.Id - 1;
                _keyPointsList = _comboBoxLocation;
                _locationBackground = Brushes.Gray;
                _locationFocus = false;
            }

            if (_comboBoxLanguage != string.Empty)
            {
                _languageIndex = (int)Enum.Parse(typeof(Language), _comboBoxLanguage);
                _languageBackground = Brushes.Gray;
                _languageFocus = false;
            }

            if (_maxGuestsText != string.Empty)
            {
                _guestsBackground = Brushes.Gray;
                _guestsFocus = false;
            }

            if (_dateList != string.Empty)
            {
                _datePick = null;
                _dateBackground = Brushes.Gray;
                _dateFocus = false;
            }
        }

        public int LanguageIndex
        {
            get => _languageIndex;
            set
            {
                _languageIndex = value;
                OnPropertyChanged();
            }
        }

        public int LocationIndex
        {
            get => _locationIndex;
            set
            {
                _locationIndex = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush LocationBackground
        {
            get => _locationBackground;
            set
            {
                _locationBackground = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush LanguageBackground
        {
            get => _languageBackground;
            set
            {
                _languageBackground = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush GuestsBackground
        {
            get => _guestsBackground;
            set
            {
                _guestsBackground = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush DateBackground
        {
            get => _dateBackground;
            set
            {
                _dateBackground = value;
                OnPropertyChanged();
            }
        }

        public bool LocationFocus
        {
            get => _locationFocus;
            set
            {
                _locationFocus = value;
                OnPropertyChanged();
            }
        }
        public bool LanguageFocus
        {
            get => _languageFocus;
            set
            {
                _languageFocus = value;
                OnPropertyChanged();
            }
        }
        public bool GuestsFocus
        {
            get => _guestsFocus;
            set
            {
                _guestsFocus = value;
                OnPropertyChanged();
            }
        }
        public bool DateFocus
        {
            get => _dateFocus;
            set
            {
                _dateFocus = value;
                OnPropertyChanged();
            }
        }

        public MyICommand<string> CreateTourCommand { get; private set; }
        public MyICommand<string> KeyPointsCommands { get; private set; }
        public MyICommand<string> ImagesCommands { get; private set; }

        private void KeyPoints(string command)
        {
            switch (command)
            {
                case "Add":
                    if (ComboBoxKeyPoints == null) break;
                    var hasText = false;

                    if (KeyPointsList != null)
                        hasText = KeyPointsList.Contains(ComboBoxKeyPoints);

                    if (KeyPointsList != null)
                        if (KeyPointsList.Contains(",, "))
                            ComboBoxKeyPoints = ComboBoxKeyPoints.Replace(",, ", ", ");

                    if (!hasText)
                    {
                        if (string.IsNullOrEmpty(KeyPointsList))
                            KeyPointsList = ComboBoxKeyPoints;

                        else
                            KeyPointsList += ", " + ComboBoxKeyPoints;
                    }

                    if (KeyPointsList != null)
                        if (KeyPointsList.StartsWith(", "))
                            KeyPointsList = KeyPointsList.Substring(2, KeyPointsList.Length - 2);

                    ComboBoxKeyPoints = "";
                    break;

                case "Delete":
                    KeyPointsList = "";
                    break;
            }
        }

        private void Images(string command)
        {
            switch (command)
            {
                case "Add":
                    if (ImagesText == null) break;
                    var hasText = false;

                    if (ImagesList != null)
                        hasText = ImagesList.Contains(ImagesText);

                    if (ImagesList != null)
                        if (ImagesList.Contains(",, "))
                            ImagesList = ImagesList.Replace(",, ", ", ");

                    if (!hasText)
                    {
                        if (string.IsNullOrEmpty(ImagesList))
                            ImagesList = ImagesText;
                        else
                            ImagesList += ", " + ImagesText;
                    }

                    if (ImagesList != null)
                        if (ImagesList.StartsWith(", "))
                            ImagesList = ImagesList.Substring(2, ImagesList.Length - 2);

                    ImagesText = "";
                    break;

                case "Delete":
                    ImagesList = "";
                    break;
            }
        }

        public void Dates(string command)
        {
            switch (command)
            {
                case "Add":
                    if (DatePick == null) break;
                    var date = Convert.ToDateTime(DatePick).ToString("MM/dd/yyyy", new CultureInfo("en-US"));
                    var hasText = false;

                    if (DateList != null)
                        hasText = DateList.Contains(date);

                    if (DateList != null)
                        if (DateList.Contains(",, "))
                            DateList = DateList.Replace(",, ", ", ");

                    if (!hasText)
                    {
                        if (string.IsNullOrEmpty(DateList))
                            DateList += date;
                        else
                            DateList += ", " + date;
                    }

                    if (DateList != null)
                        if (DateList.StartsWith(", "))
                            DateList = DateList.Substring(2, DateList.Length - 2);

                    DatePick = DateTime.Today;
                    break;

                case "Delete":
                    DateList = "";
                    break;
            }
        }


        private void Confirm(string create)
        {
            if (!IsCreateTourEnabled) return;
            if (create != "Create" && create != "Button") return;
            var currentLocation = _locationService.GetByCity(ComboBoxLocation!);
            var maxGuests = int.Parse(MaxGuestsText!);
            var duration = float.Parse(DurationText!);
            var cities = new List<string>(KeyPointsList!.Split(", "));
            var locationList = _locationService.GetByAllCities(cities);
            var language = _tourService.FindLanguage(ComboBoxLanguage!);

            if (currentLocation != null)
            {
                foreach(var tour in _tourService.GetAllAsCollection())
                {
                    if (tour.Name != NameText!) continue;
                    NameText = "";
                    ErrorMessageText = "Tour with the name " + NameText! + " already exists!";
                    return;
                }
                
                _tourService.Add(new Tour(NameText!, currentLocation,
                DescriptionText!, language, maxGuests, locationList, DateList!,
                duration, ImagesList!));
                MessageBox.Show("Tour Added Successfully.");

                if (CreateAcceptedTourDto.RequestId != -1)
                {
                    _requestTourService.ConfirmRequest(CreateAcceptedTourDto.RequestId,
                        CreateAcceptedTourDto.DateList.Split(",")[0]);

                    CreateAcceptedTourDto.RequestId = -1;
                }

                if (CreateAcceptedTourDto.IsFromStatistics)
                {
                    var notificationsService = new TouristNotificationService();
                    notificationsService.NotifyForNewTours(NameText!, currentLocation, language);
                }
            }

            else
                MessageBox.Show("UNSUCCESSFUL TOUR");

            var mainWindow = Application.Current.Windows.OfType<Guide>().FirstOrDefault();
            if (mainWindow!.DataContext is GuideViewModel guideViewModel)
                guideViewModel.CurrentViewModel = new HomePageViewModel();

            CreateAcceptedTourDto.Location = string.Empty;
            CreateAcceptedTourDto.Language = string.Empty;
            CreateAcceptedTourDto.MaxGuests = string.Empty;
            CreateAcceptedTourDto.DateList = string.Empty;

        }

        public string? ErrorMessageText
        {
            get => _errorMessageText;
            set
            {
                _errorMessageText = value;
                OnPropertyChanged();
            }
        }

        public bool IsCreateTourEnabled
        {
            get => _isCreateTourEnabled;
            set
            {
                _isCreateTourEnabled = value;
                OnPropertyChanged();
            }
        }

        public string? NameText
        {
            get => _nameText;
            set
            {
                _nameText = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public string? ComboBoxLocation
        {
            get => _comboBoxLocation;
            set
            {
                _comboBoxLocation = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public string? DescriptionText
        {
            get => _descriptionText;
            set
            {
                _descriptionText = value;
                OnPropertyChanged();
            }
        }

        public string? ComboBoxLanguage
        {
            get => _comboBoxLanguage;
            set
            {
                _comboBoxLanguage = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public string? MaxGuestsText
        {
            get => _maxGuestsText;
            set
            {
                _maxGuestsText = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public string? KeyPointsList
        {
            get => _keyPointsList;
            set
            {
                _keyPointsList = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public string? ComboBoxKeyPoints
        {
            get => _comboBoxKeyPoints;
            set
            {
                _comboBoxKeyPoints = value;
                OnPropertyChanged();
            }
        }

        public string? DateList
        {
            get => _dateList;
            set
            {
                _dateList = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public DateTime? DatePick
        {
            get => _datePick;
            set
            {
                _datePick = value;
                OnPropertyChanged();
            }
        }

        public string? DurationText
        {
            get => _durationText;
            set
            {
                _durationText = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public string? ImagesList
        {
            get => _imagesList;
            set
            {
                _imagesList = value;
                OnPropertyChanged();
                Validate();
            }
        }
        public string? ImagesText
        {
            get => _imagesText;
            set
            {
                _imagesText = value;
                OnPropertyChanged();
            }
        }

        private void Validate()
        {
            ErrorMessageText = string.Empty;

            var isImagesValid = !string.IsNullOrEmpty(ImagesList);

            if (isImagesValid)
            {
                var imageUrls = ImagesList!.Split(',');

                foreach (string imageUrl in imageUrls)
                {
                    if (Uri.TryCreate(imageUrl, UriKind.Absolute, out Uri? result) &&
                        (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps)) continue;
                    isImagesValid = false;
                    break;
                }
            }

            if (!isImagesValid)
                ErrorMessageText = "Please add at least one image for the tour (*NOTE - Image links have to be valid!).";

            var isDurationValid = !string.IsNullOrEmpty(DurationText) && Regex.IsMatch(DurationText, @"^\d+(\.\d+)?$");
            if (!isDurationValid)
                ErrorMessageText = "Please enter a valid duration (in hours) for the tour (e.g. 2.5).";

            var isDateValid = !string.IsNullOrEmpty(DateList);
            if (!isDateValid)
                ErrorMessageText = "Please add at least one date for the tour.";

            var isKeyPointsValid = ComboBoxLocation != null && !string.IsNullOrEmpty(KeyPointsList) && KeyPointsList.Contains(",") && KeyPointsList.Split(", ")[0].Contains(ComboBoxLocation);
            if (!isKeyPointsValid)
                ErrorMessageText = "Please add at least two key points (separated by commas) for the tour (*NOTE - First Key Point HAS to be the same as Location).";

            var isMaxGuestsValid = !string.IsNullOrEmpty(MaxGuestsText) && Regex.IsMatch(MaxGuestsText, "^[0-9]+$");
            if (!isMaxGuestsValid)
                ErrorMessageText = "Please enter a valid number of maximum guests for the tour (must be a positive integer).";

            var isLanguageValid = !string.IsNullOrEmpty(ComboBoxLanguage);
            if (!isLanguageValid)
                ErrorMessageText = "Please select a language for the tour.";

            var isLocationValid = !string.IsNullOrEmpty(ComboBoxLocation);
            if (!isLocationValid)
                ErrorMessageText = "Please select a location for the tour.";

            var isNameValid = !string.IsNullOrEmpty(NameText) && NameText.Length >= 3;
            if (!isNameValid)
                ErrorMessageText = "Please enter a valid name for the tour (must be at least 3 characters long).";


            IsCreateTourEnabled = isImagesValid && isLocationValid && isLanguageValid && isMaxGuestsValid && isKeyPointsValid && isDateValid && isDurationValid && isNameValid;

            if (IsCreateTourEnabled)
            {
                ErrorMessageText = "Press Right Shift To Finalize Creation";
                ErrorTextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#68cbf8"));
            }
            OnPropertyChanged(nameof(IsCreateTourEnabled));
            OnPropertyChanged(nameof(ErrorMessageText));
        }

        public SolidColorBrush ErrorTextColor
        {
            get => _errorTextColor;
            set
            {
                _errorTextColor = value;
                OnPropertyChanged();
            }
        }

    }
}
