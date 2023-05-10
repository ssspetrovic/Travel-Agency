using System.Linq;
using System.Windows;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.View;
using TravelAgency.View.Controls.Guide;

namespace TravelAgency.ViewModel
{
    public class GuideViewModel : BaseViewModel
    {
        
        private BaseViewModel _currentViewModel = new HomePageViewModel();
        
        public string CurrentViewName { get; set; }

        public GuideViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            ReserveCommand = new MyICommand<string>(OnNav);
            LogOutCommand = new MyICommand(LogOut);
            ResignCommand = new MyICommand(Resign);
            VideoTutorialCommand = new MyICommand(VideoCommand);
            CurrentViewModel = _currentViewModel;
            CurrentViewName = "All Tours";
        }

        public MyICommand<string> NavCommand { get; private set; }
        public MyICommand<string> ReserveCommand { get; private set; }
        public MyICommand LogOutCommand { get; private set; }
        public MyICommand ResignCommand { get; private set; }
        public MyICommand VideoTutorialCommand { get; private set; }

        public string LoadCurrentUserData => "Welcome " + CurrentUser.DisplayName;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        private void VideoCommand()
        {
            var videoTutorialViewModel = new VideoTutorialViewModel();
            var currentWindow = Application.Current.Windows.OfType<Guide>().FirstOrDefault();
            videoTutorialViewModel.Url += currentWindow!.Title != "Shortcuts" ? currentWindow!.Title + ".mp4" : "video.mp4";

            var videoTutorial = new VideoTutorial
            {
                DataContext = videoTutorialViewModel,
            };

            videoTutorial.ShowDialog();
        }

        public void OnNav(string destination)
        {
            var currentWindow = Application.Current.Windows.OfType<Guide>().FirstOrDefault();
            var newWindow = new Guide();
            if (newWindow.DataContext is not GuideViewModel guideViewModel) return;
            switch (destination)
            {
                case "Home Page":
                    guideViewModel.CurrentViewModel = new HomePageViewModel();
                    break;
                case "Create Tour":
                    guideViewModel.CurrentViewModel = new CreateTourViewModel();
                    CreateAcceptedTourDto.Location = string.Empty;
                    CreateAcceptedTourDto.Language = string.Empty;
                    CreateAcceptedTourDto.MaxGuests = string.Empty;
                    CreateAcceptedTourDto.DateList = string.Empty;
                    break;
                case "Monitor Tour":
                    guideViewModel.CurrentViewModel = new MonitorTourViewModel();
                    break;
                case "Cancel Tour":
                    guideViewModel.CurrentViewModel = new CancelTourViewModel();
                    break;
                case "Tour Stats":
                    guideViewModel.CurrentViewModel = new TourStatsViewModel();
                    break;
                case "Tour Review":
                    guideViewModel.CurrentViewModel = new ReviewTourViewModel();
                    break;
                case "Accept Tour Requests":
                    guideViewModel.CurrentViewModel = new AcceptTourViewModel();
                    break;
                case "Complex Tour Requests":
                    guideViewModel.CurrentViewModel = new ComplexTourViewModel();
                    break;
                case "Request Stats":
                    guideViewModel.CurrentViewModel = new RequestStatsViewModel();
                    break;
                case "Create Suggested Tour":
                    guideViewModel.CurrentViewModel = new CreateSuggestedTourViewModel();
                    break;
                case "Shortcuts":
                    guideViewModel.CurrentViewModel = new ShortcutsViewModel();
                    break;
            }
            newWindow.Title = destination;
            newWindow.Show();
            currentWindow!.Close();
        }

        private void LogOut()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)!;
            var signInView = new SignInView();
            signInView.Show();
            currentWindow.Close();
        }

        private void Resign()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)!;
            var resignationView = new Resign();
            resignationView.Show();
            currentWindow.Close();
        }
    }
}
