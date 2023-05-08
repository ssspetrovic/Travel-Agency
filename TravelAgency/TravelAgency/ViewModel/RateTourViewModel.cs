using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View;
using TravelAgency.View.Controls.Tourist;
using static System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace TravelAgency.ViewModel
{
    internal class RateTourViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private int _guideKnowledgeGrade;
        private int _guideLanguageGrade;
        private int _tourInterestingnessGrade;
        private string? _comment;
        private string? _photoUrls;
        private string? _url;
        public string TourNameHeader { get; }
        private string TourName { get; }
        public RelayCommand CancelRatingCommand { get; set; }
        public RelayCommand SubmitRatingCommand { get; set; }
        public RelayCommand AddPhotoCommand { get; set; }

        public int GuideKnowledgeGrade
        {
            get => _guideKnowledgeGrade;
            set
            {
                _guideKnowledgeGrade = value;
                OnPropertyChanged();
            }
        }

        public int GuideLanguageGrade
        {
            get => _guideLanguageGrade;
            set
            {
                _guideLanguageGrade = value;
                OnPropertyChanged();
            }
        }

        public int TourInterestingnessGrade
        {
            get => _tourInterestingnessGrade;
            set
            {
                _tourInterestingnessGrade = value;
                OnPropertyChanged();
            }
        }

        public string? Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }
        public string? PhotoUrls
        {
            get => _photoUrls;
            set
            {
                _photoUrls = value;
                OnPropertyChanged();
            }
        }

        public string? Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }

        public RateTourViewModel(NavigationService navigationService, string tourName)
        {
            _navigationService = navigationService;
            TourName = tourName;
            CancelRatingCommand =
                new RelayCommand(Execute_CancelRatingCommand, CanExecute_CancelRatingCommand);
            SubmitRatingCommand =
                new RelayCommand(Execute_SubmitRatingCommand, CanExecute_SubmitRatingCommand);
            AddPhotoCommand =
                new RelayCommand(Execute_AddPhotoCommand, CanExecute_AddPhotoCommand);

            if (TourNameHeader == "/")
            {
                MessageBox.Show("Error with selected tour!", "Error");
                return;
            }

            TourNameHeader = $"'{tourName}'";
        }

        private void Execute_CancelRatingCommand(object parameter)
        {
            _navigationService.Navigate(new MyToursView());
        }

        private void Execute_SubmitRatingCommand(object parameter)
        {
            var touristService = new TouristService();
            var tourRatingService = new TourRatingService();
            var tourService = new TourService();
            var tourist = touristService.GetByUsername(CurrentUser.Username);

            tourRatingService.Add(new TourRating(
                tourist.Id,
                tourService.GetByName(TourName).Id,
                (Grade)GuideKnowledgeGrade,
                (Grade)GuideLanguageGrade,
                (Grade)TourInterestingnessGrade,
                Comment ?? "",
                PhotoUrls ?? ""
            ));

            var myTourDtoService = new MyTourDtoService();
            myTourDtoService.UpdateStatus(TourName, MyTourDto.TourStatus.Rated);
            _navigationService.Navigate(new MyToursView());
        }

        private void Execute_AddPhotoCommand(object parameter)
        {
            var formattedUri = Url!.Trim();
            formattedUri = $"{formattedUri};\r\n";
            PhotoUrls += formattedUri;
            Url = string.Empty;
        }

        private bool CanExecute_CancelRatingCommand(object parameter)
        {
            return true;
        }

        private bool CanExecute_SubmitRatingCommand(object parameter)
        {
            return GuideKnowledgeGrade == 0 || GuideLanguageGrade == 0 || TourInterestingnessGrade == 0;
        }

        private bool CanExecute_AddPhotoCommand(object parameter)
        {
            return !string.IsNullOrEmpty(Url);
        }

        public void Submit()
        {
            if (GuideKnowledgeGrade == 0 || GuideLanguageGrade == 0 || TourInterestingnessGrade == 0)
            {
                MessageBox.Show("Please select ratings properly!", "Error");
                return;
            }

            var touristService = new TouristService();
            var tourRatingService = new TourRatingService();
            var tourService = new TourService();
            var tourist = touristService.GetByUsername(CurrentUser.Username);

            tourRatingService.Add(new TourRating(
                tourist.Id,
                tourService.GetByName(TourName).Id,
                (Grade)GuideKnowledgeGrade,
                (Grade)GuideLanguageGrade,
                (Grade)TourInterestingnessGrade,
                Comment ?? "",
                PhotoUrls ?? ""
            ));

            var myTourDtoService = new MyTourDtoService();

            myTourDtoService.UpdateStatus(TourName, MyTourDto.TourStatus.Rated);
            var mainWindow = new TouristView
            {
                ContentFrame =
                {
                    Source = new Uri("Controls/Tourist/MyToursView.xaml", UriKind.Relative)
                }
            };
            var currentWindow = Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Show();
            currentWindow?.Close();
        }

        public void AddUrl()
        {
            if (string.IsNullOrEmpty(Url))
            {
                MessageBox.Show("Invalid link!", "Error");
                return;
            }

            var formattedUri = Url.Trim();
            formattedUri = $"{formattedUri};\r\n";
            PhotoUrls += formattedUri;
            Url = string.Empty;
        }
    }
}
