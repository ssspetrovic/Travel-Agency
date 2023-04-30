namespace TravelAgency.ViewModel
{
    public class ShowMonthlyRequestStatsViewModel : BaseViewModel
    {
        public ShowMonthlyRequestStatsViewModel()
        {
            _currentMonth = "";
            _numberOfRequests = "";
        }

        private string _currentMonth;
        public string CurrentMonth
        {
            get => _currentMonth;
            set
            {
                _currentMonth = value;
                OnPropertyChanged();
            }
        }

        private string _numberOfRequests;
        public string NumberOfRequests
        {
            get => _numberOfRequests;
            set
            {
                _numberOfRequests = value;
                OnPropertyChanged();
            }
        }


    }
}
