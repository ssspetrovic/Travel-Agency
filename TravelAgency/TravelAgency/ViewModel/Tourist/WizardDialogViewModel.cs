using System;
using System.Collections.ObjectModel;
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
        private readonly ObservableCollection<string> _imagePaths;
        private int _currentImageIndex;

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

        public WizardDialogViewModel()
        {
            var folderPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FolderString));

            _imagePaths = new ObservableCollection<string>
            {
                Path.Combine(folderPath, "Navigation.jpg"),
                Path.Combine(folderPath, "User.jpg"),
                Path.Combine(folderPath, "Home page.jpg"),
                Path.Combine(folderPath, "Notifications.jpg"),
                Path.Combine(folderPath, "My tours.jpg"),
                Path.Combine(folderPath, "Rate a tour.jpg"),
                Path.Combine(folderPath, "My vouchers.jpg"),
                Path.Combine(folderPath, "My requests.jpg"),
                Path.Combine(folderPath, "Requests statistics.jpg"),
                Path.Combine(folderPath, "Browse tours.jpg"),
                Path.Combine(folderPath, "Tour overview.jpg"),
                Path.Combine(folderPath, "Request a tour.jpg"),
                Path.Combine(folderPath, "Regular tour request.jpg"),
            };

            _currentImageIndex = 0;
            _currentImage = new BitmapImage(new Uri(_imagePaths[_currentImageIndex], UriKind.Absolute));

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
        }

        private void Execute_FinishCommand(object parameter)
        {
            var windowManager = new WindowManager();
            windowManager.ShowWindow<TouristView>();
            windowManager.CloseWindow<WizardDialogView>();
        }
    }
}
