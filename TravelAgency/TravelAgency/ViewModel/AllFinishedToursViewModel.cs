using System.Data;
using System.Linq;
using System.Windows;
using TravelAgency.Service;
using TravelAgency.View.Controls.Guide;
using TravelAgency.View;
using TravelAgency.Model;

namespace TravelAgency.ViewModel
{
    public class AllFinishedToursViewModel : HomePageViewModel
    {

        private readonly FinishedTourService _finishedTourService;

        public AllFinishedToursViewModel()
        {
            _finishedTourService = new FinishedTourService();
            NavCommand = new MyICommand<string>(OnNav);
            SelectFinishedTourCommand = new MyICommand(SelectTour);
        }

        public MyICommand<string> NavCommand { get; private set; }
        public MyICommand SelectFinishedTourCommand { get; private set; }
        public string LoadCurrentUserData => "Welcome " + CurrentUser.DisplayName;

        public void SelectTour()
        {
            CurrentFinishedTour.Name = SelectedTour == null ? FinishedTours[0]["Name"].ToString() : SelectedTour["Name"].ToString()!;
            var window = Application.Current.Windows.OfType<AllFinishedTours>().FirstOrDefault();
            var selectedFinishedTour = new SelectedFinishedTour();
            selectedFinishedTour.Show();
            window!.Close();
        }

        public void OnNav(string command)
        {
            var window = Application.Current.Windows.OfType<AllFinishedTours>().FirstOrDefault();
            var mainWindow = new Guide();
            if (mainWindow.DataContext is not GuideViewModel guideViewModel) return;
            switch (command) 
            {
                case "HomePage":
                    guideViewModel.CurrentViewModel = new HomePageViewModel();
                    mainWindow.Title = "Home Page";
                    mainWindow.Show();
                    window!.Close();
                    break;
                case "TourStats":
                    guideViewModel.CurrentViewModel = new TourStatsViewModel();
                    mainWindow.Title = "Tour Stats";
                    mainWindow.Show();
                    window!.Close();
                    break;
            }
        }

        public DataView FinishedTours
        {
            get
            {
                var dt = new DataTable();
                dt = _finishedTourService.GetAllAsDataTable(dt);
                ConvertTourColumn(dt, "KeyPointsList", typeof(string), "KeyPoints");

                return dt.DefaultView;
            }
        }

    }
}
