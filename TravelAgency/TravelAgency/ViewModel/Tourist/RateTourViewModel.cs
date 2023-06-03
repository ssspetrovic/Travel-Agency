using System.ComponentModel;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Tourist;
using static System.Windows.Application;

namespace TravelAgency.ViewModel.Tourist
{
    internal class RateTourViewModel : BaseViewModel
    {
        private readonly TouristViewModel _touristViewModel;
        private readonly NavigationService _navigationService;
        private int _guideKnowledgeGrade;
        private int _guideLanguageGrade;
        private int _tourInterestingnessGrade;
        private string? _comment;
        private string? _photoUrls;
        private string? _url;
        public string TourNameHeader { get; }
        private string TourName { get; }
        private OkDialog? Dialog { get; set; }

        public RelayCommand CancelRatingCommand { get; set; }
        public RelayCommand SubmitRatingCommand { get; set; }
        public RelayCommand AddPhotoCommand { get; set; }
        private RelayCommand NavigateToMyToursCommand { get; set; }
        private bool _isTooltipsSwitchToggled;

        public bool IsTooltipsSwitchToggled
        {
            get => _isTooltipsSwitchToggled;
            set
            {
                _isTooltipsSwitchToggled = value;
                OnPropertyChanged();
            }
        }

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

        public RateTourViewModel(NavigationService navigationService, TouristViewModel touristViewModel, string tourName)
        {
            _touristViewModel = touristViewModel;
            IsTooltipsSwitchToggled = _touristViewModel.IsTooltipsSwitchToggled;
            _touristViewModel.PropertyChanged += TouristViewModel_PropertyChanged;;
            _navigationService = navigationService;
            TourName = tourName;
            TourNameHeader = $"'{tourName}'";

            CancelRatingCommand = new RelayCommand(Execute_CancelRatingCommand);
            SubmitRatingCommand = new RelayCommand(Execute_SubmitRatingCommand, CanExecute_SubmitRatingCommand);
            AddPhotoCommand = new RelayCommand(Execute_AddPhotoCommand, CanExecute_AddPhotoCommand);
            NavigateToMyToursCommand = new RelayCommand(Execute_NavigateToMyToursCommand);
        }

        private void TouristViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(TouristViewModel.IsTooltipsSwitchToggled)) return;
            if (sender != null) IsTooltipsSwitchToggled = ((TouristViewModel)sender).IsTooltipsSwitchToggled;
        }

        private void Execute_CancelRatingCommand(object parameter)
        {
            _navigationService.GoBack();
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
            Dialog = new OkDialog
            {
                Owner = Current.MainWindow,
                TextBlock = 
                {
                    Text = "Tour successfully rated!"
                },
                Button =
                {
                    Command = NavigateToMyToursCommand
                }
            };
            Dialog.Show();
        }

        private void Execute_AddPhotoCommand(object parameter)
        {
            var formattedUri = Url!.Trim();
            formattedUri = $"{formattedUri};\r\n";
            PhotoUrls += formattedUri;
            Url = string.Empty;
        }

        private void Execute_NavigateToMyToursCommand(object parameter)
        {
            _navigationService.Navigate(new MyToursView(_navigationService, _touristViewModel));
            Dialog?.Close();
        }

        private bool CanExecute_SubmitRatingCommand(object parameter)
        {
            return GuideKnowledgeGrade != 0 && GuideLanguageGrade != 0 && TourInterestingnessGrade != 0;
        }

        private bool CanExecute_AddPhotoCommand(object parameter)
        {
            return !string.IsNullOrEmpty(Url);
        }
    }
}
