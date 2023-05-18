using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using TravelAgency.Command;
using TravelAgency.View.Tourist;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel.Tourist
{
    internal class WizardDialogViewModel : BaseViewModel
    {
        private const string FolderString = @"..\..\..\Resources\Images\TouristWizardImages\";
        private readonly List<string> _imagePaths;
        private int _currentImageIndex;

        private readonly List<string> _imageNames = new()
        {
            "Navigation",
            "User profile",
            "Home page",
            "Notifications",
            "My tours",
            "Rate a tour",
            "My vouchers",
            "My requests",
            "Requests statistics",
            "Browse tours",
            "Tour overview",
            "Request a tour",
            "Regular tour request"
        };

        public RelayCommand NextCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand FinishCommand { get; set; }

        private BitmapImage _currentImage;
        public BitmapImage CurrentImage
        {
            get => _currentImage;
            set
            {
                _currentImage = value;
                OnPropertyChanged();
            }
        }

        private string _currentPageLabel;

        public string CurrentPageLabel
        {
            get => _currentPageLabel;
            set
            {
                _currentPageLabel = value;
                OnPropertyChanged();
            }
        }

        public WizardDialogViewModel()
        {
            var folderPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FolderString));

            _imagePaths = new List<string>();

            foreach (var photo in _imageNames)
            {
                _imagePaths.Add(Path.Combine(folderPath, photo + ".jpg"));
            }

            _currentImageIndex = 0;
            _currentImage = new BitmapImage(new Uri(_imagePaths[_currentImageIndex], UriKind.Absolute));
            _currentPageLabel = _imageNames[_currentImageIndex];

            NextCommand = new RelayCommand(Execute_NextCommand, CanExecute_NextCommand);
            BackCommand = new RelayCommand(Execute_BackCommand, CanExecute_BackCommand);
            FinishCommand = new RelayCommand(Execute_FinishCommand);
        }

        private void Execute_NextCommand(object parameter)
        {
            _currentImageIndex++;
            LoadCurrentImage();
        }

        private bool CanExecute_NextCommand(object parameter)
        {
            return _currentImageIndex < _imagePaths.Count - 1;
        }

        private void Execute_BackCommand(object parameter)
        {
            _currentImageIndex--;
            LoadCurrentImage();
        }

        private bool CanExecute_BackCommand(object parameter)
        {
            return _currentImageIndex > 0;
        }

        private void LoadCurrentImage()
        {
            var imagePath = _imagePaths[_currentImageIndex];
            CurrentImage = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            CurrentPageLabel = _imageNames[_currentImageIndex];
        }

        private void Execute_FinishCommand(object parameter)
        {
            var windowManager = new WindowManager();
            windowManager.ShowWindow<TouristView>();
            windowManager.CloseWindow<WizardDialogView>();
        }
    }
}
