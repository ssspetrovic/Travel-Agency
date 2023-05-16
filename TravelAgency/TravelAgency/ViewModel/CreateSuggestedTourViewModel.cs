using System.Linq;
using System.Windows;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View;

namespace TravelAgency.ViewModel
{
    public class CreateSuggestedTourViewModel : HomePageViewModel
    {
        private readonly RequestTourService _requestTourService;
        private readonly LocationService _locationService;

        public CreateSuggestedTourViewModel()
        {
            _requestTourService = new RequestTourService();
            _locationService = new LocationService();
            CreateLocation = new MyICommand(CreateLoc);
            CreateLanguage = new MyICommand(CreateLang);
        }

        public MyICommand CreateLocation { get; private set; }
        public MyICommand CreateLanguage { get; private set; }

        public void CreateLoc()
        {
            CreateAcceptedTourDto.Language = "";
            CreateAcceptedTourDto.Location = MostRequestedLocation.Split(", ")[0];
            CreateTourView();
        }

        public void CreateLang()
        {
            CreateAcceptedTourDto.Location = "";
            CreateAcceptedTourDto.Language = MostRequestedLanguage;
            CreateTourView();
        }

        public void CreateTourView()
        {
            var currentWindow = Application.Current.Windows.OfType<Guide>().FirstOrDefault();
            var newWindow = new Guide();
            if (newWindow.DataContext is not GuideViewModel guideViewModel) return;
            CreateAcceptedTourDto.IsFromStatistics = true;
            guideViewModel.CurrentViewModel = new CreateTourViewModel();
            newWindow.Title = "Create Tour";
            newWindow.Show();
            currentWindow!.Close();
        }

        public string MostRequestedLocation
        {
            get
            {
                var location =
                    _locationService.GetById(int.Parse(_requestTourService.GetMostRequestedStat("Location_Id")));
                return location!.City + ", " + location.Country;
            }
        }
        public string MostRequestedLanguage => ((Language)int.Parse(_requestTourService.GetMostRequestedStat("Language"))).ToString();
    }
}
