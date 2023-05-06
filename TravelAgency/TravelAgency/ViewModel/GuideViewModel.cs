using System.Linq;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.View;
using TravelAgency.View.Controls.Guide;

namespace TravelAgency.ViewModel
{
    public class GuideViewModel : BaseViewModel
    {
        public MyICommand<string> NavCommand { get; private set; }
        public MyICommand<string> ReserveCommand { get; private set; }
        private BaseViewModel _currentViewModel = new HomePageViewModel();
        public MyICommand LogOutCommand { get; private set; }
        public MyICommand ResignCommand { get; private set; }

        public GuideViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            ReserveCommand = new MyICommand<string>(OnReserve);
            CurrentViewModel = _currentViewModel;
            LogOutCommand = new MyICommand(LogOut);
            ResignCommand = new MyICommand(Resign);
        }

        public string LoadCurrentUserData => "Welcome " + CurrentUser.DisplayName;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        private void OnReserve(string destination)
        {
            switch (destination)
            {
                case "HomePage":
                    CurrentViewModel = new HomePageViewModel();
                    break;
                case "CreateTour":
                    CurrentViewModel = new CreateTourViewModel();
                    break;
                case "Monitor":
                    CurrentViewModel = new MonitorTourViewModel();
                    break;
                case "CancelTour":
                    CurrentViewModel = new CancelTourViewModel();
                    break;
                case "TourStats":
                    CurrentViewModel = new TourStatsViewModel();
                    break;
                case "TourReview":
                    CurrentViewModel = new ReviewTourViewModel();
                    break;
                case "AcceptTour":
                    CurrentViewModel = new AcceptTourViewModel();
                    break;
                case "ComplexTour":
                    CurrentViewModel = new ComplexTourViewModel();
                    break;
                case "RequestStats":
                    CurrentViewModel = new RequestStatsViewModel();
                    break;
                case "CreateSuggested":
                    CurrentViewModel = new CreateSuggestedTourViewModel();
                    break;
                case "LogOut":
                    LogOut();
                    break;
                case "Resign":
                    Resign();
                    break;
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "HomePage":
                    CurrentViewModel = new HomePageViewModel();
                    break;
                case "CreateTour":
                    CurrentViewModel = new CreateTourViewModel();
                    break;
                case "Monitor":
                    CurrentViewModel = new MonitorTourViewModel();
                    break;
                case "CancelTour":
                    CurrentViewModel = new CancelTourViewModel();
                    break;
                case "TourStats":
                    CurrentViewModel = new TourStatsViewModel();
                    break;
                case "TourReview":
                    CurrentViewModel = new ReviewTourViewModel();
                    break;
                case "AcceptTour":
                    CurrentViewModel = new AcceptTourViewModel();
                    break;
                case "ComplexTour":
                    CurrentViewModel = new ComplexTourViewModel();
                    break;
                case "RequestStats":
                    CurrentViewModel = new RequestStatsViewModel();
                    break;
                case "CreateSuggested":
                    CurrentViewModel = new CreateSuggestedTourViewModel();
                    break;
            }
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
