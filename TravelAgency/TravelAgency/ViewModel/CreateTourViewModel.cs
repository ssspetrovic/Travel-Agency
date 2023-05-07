﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View;
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

        private readonly TourService _tourService;
        private readonly LocationService _locationService;

        public CreateTourViewModel()
        {
            _tourService = new TourService();
            _locationService = new LocationService();
            CreateTourCommand = new MyICommand<string>(Confirm);
            KeyPointsCommands = new MyICommand<string>(KeyPoints);
            ImagesCommands = new MyICommand<string>(Images);
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

                    if(KeyPointsList != null)
                        if (KeyPointsList.Contains(",, "))
                            ComboBoxKeyPoints = ComboBoxKeyPoints.Replace(",, ", ", ");

                    if (!hasText)
                    {
                        if (string.IsNullOrEmpty(KeyPointsList))
                            KeyPointsList = ComboBoxKeyPoints;
                        
                        else
                            KeyPointsList += ", " + ComboBoxKeyPoints;
                    }

                    if(KeyPointsList != null)
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

                    if(ImagesList != null)
                       hasText = ImagesList.Contains(ImagesText);

                    if(ImagesList != null)
                        if (ImagesList.Contains(",, "))
                            ImagesList = ImagesList.Replace(",, ", ", ");

                    if (!hasText)
                    {
                        if (string.IsNullOrEmpty(ImagesList))
                            ImagesList = ImagesText;
                        else
                            ImagesList += ", " + ImagesText;
                    }                        

                    if(ImagesList != null)
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

                    if(DateList != null)
                        hasText = DateList.Contains(date);

                    if(DateList != null)
                        if (DateList.Contains(",, "))
                            DateList = DateList.Replace(",, ", ", ");

                    if (!hasText)
                    {
                        if (string.IsNullOrEmpty(DateList))
                            DateList += date;
                        else
                            DateList += ", " + date;
                    }

                    if(DateList != null)
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
            if (create != "Create" && create != "Button") return;
            var currentLocation = _locationService.GetByCity(ComboBoxLocation!);
            var maxGuests = int.Parse(MaxGuestsText!);
            var duration = float.Parse(DurationText!);
            var cities = new List<string>(KeyPointsList!.Split(", "));
            var locationList = _locationService.GetByAllCities(cities);
            var language = _tourService.FindLanguage(ComboBoxLanguage!);

            if (currentLocation != null)
            {
                _tourService.Add(new Tour(NameText!, currentLocation,
                    DescriptionText!, language, maxGuests, locationList, DateList!,
                    duration, ImagesList!));
                MessageBox.Show("Tour Added Successfully.");
            }

            else
                MessageBox.Show("UNSUCCESSFUL TOUR");

            var mainWindow = Application.Current.Windows.OfType<Guide>().FirstOrDefault();
            if (mainWindow!.DataContext is GuideViewModel guideViewModel)
                guideViewModel.CurrentViewModel = new MonitorTourViewModel();
            
        }

        private SolidColorBrush _nameTextColor = new((Color)ColorConverter.ConvertFromString("#68cbf8"));

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
                if (string.IsNullOrEmpty(value) || value.Length < 3)
                {
                    FocusControl(nameof(NameText));
                    _nameTextColor = Brushes.Red;
                }
                else
                    _nameTextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#68cbf8"));

                _nameText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NameTextColor));
                Validate();
            }
        }

        public string? ComboBoxLocation
        {
            get => _comboBoxLocation;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    FocusControl(nameof(ComboBoxLocation));
                }
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
                if (string.IsNullOrEmpty(value))
                {
                    FocusControl(nameof(ComboBoxLanguage));
                }
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
                if (string.IsNullOrEmpty(value))
                {
                    FocusControl(nameof(MaxGuestsText));
                }
                else if (!int.TryParse(value, out int maxGuests) || maxGuests < 1)
                {
                    FocusControl(nameof(MaxGuestsText));
                    IsCreateTourEnabled = false;
                }
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
                if (string.IsNullOrEmpty(value))
                {
                    FocusControl(nameof(KeyPointsList));
                }
                else if (!value.Contains(","))
                {
                    FocusControl(nameof(KeyPointsList));
                }
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
                if (string.IsNullOrEmpty(value))
                {
                    FocusControl(nameof(DateList));
                }
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
                if (string.IsNullOrEmpty(value))
                {
                    FocusControl(nameof(DurationText));
                }
                else if (!double.TryParse(value, out double duration) || duration < 1)
                {
                    FocusControl(nameof(DurationText));
                }
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
                if (string.IsNullOrEmpty(value))
                {
                    FocusControl(nameof(ImagesList));
                }
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

        private void FocusControl(string error)
        {
            switch (error)
            {
                case nameof(NameText):
                    //
                    break;
                case nameof(ComboBoxLocation):
                    // Focus ComboBoxLocation control
                    break;
                case nameof(ComboBoxLanguage):
                    // Focus ComboBoxLanguage control
                    break;
                case nameof(MaxGuestsText):
                    // Focus MaxGuestsTextBox control
                    break;
                case nameof(KeyPointsList):
                    // Focus KeyPointsTextBox control
                    break;
                case nameof(DateList):
                    // Focus DateTextBox control
                    break;
                case nameof(DurationText):
                    // Focus DurationTextBox control
                    break;
                case nameof(ImagesList):
                    // Focus ImagesTextBox control
                    break;
            }
        }

        private void Validate()
        {
            ErrorMessageText = string.Empty;

            bool isImagesValid = !string.IsNullOrEmpty(ImagesList);
            if (!isImagesValid)
                ErrorMessageText = "Please add at least one image for the tour.";

            bool isDateValid = !string.IsNullOrEmpty(DateList);
            if (!isDateValid)
                ErrorMessageText = "Please add at least one date for the tour.";

            bool isDurationValid = !string.IsNullOrEmpty(DurationText) && Regex.IsMatch(DurationText, @"^\d+(\.\d+)?$");
            if (!isDurationValid)
                ErrorMessageText = "Please enter a valid duration (in hours) for the tour (e.g. 2.5).";

            bool isKeyPointsValid = !string.IsNullOrEmpty(KeyPointsList) && KeyPointsList.Contains(",");
            if (!isKeyPointsValid)
                ErrorMessageText = "Please add at least one key point (separated by commas) for the tour.";

            bool isMaxGuestsValid = !string.IsNullOrEmpty(MaxGuestsText) && Regex.IsMatch(MaxGuestsText, "^[0-9]+$");
            if (!isMaxGuestsValid)
                ErrorMessageText = "Please enter a valid number of maximum guests for the tour (must be a positive integer).";

            bool isLanguageValid = !string.IsNullOrEmpty(ComboBoxLanguage);
            if (!isLanguageValid)
                ErrorMessageText = "Please select a language for the tour.";

            bool isLocationValid = !string.IsNullOrEmpty(ComboBoxLocation);
            if (!isLocationValid)
                ErrorMessageText = "Please select a location for the tour.";

            bool isNameValid = !string.IsNullOrEmpty(NameText) && NameText.Length >= 3;
            if (!isNameValid)
                ErrorMessageText = "Please enter a valid name for the tour (must be at least 3 characters long).";


            IsCreateTourEnabled = isImagesValid && isLocationValid && isLanguageValid && isMaxGuestsValid && isKeyPointsValid && isDateValid && isDurationValid && isNameValid;

            OnPropertyChanged(nameof(IsCreateTourEnabled));
            OnPropertyChanged(nameof(ErrorMessageText));
        }



        public SolidColorBrush NameTextColor
        {
            get => _nameTextColor;
            set
            {
                _nameTextColor = value;
                OnPropertyChanged();
            }
        }

    }
}
