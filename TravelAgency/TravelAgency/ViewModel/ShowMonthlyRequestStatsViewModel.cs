using TravelAgency.View.Controls.Guide;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
{
    public class ShowMonthlyRequestStatsViewModel : BaseViewModel
    {
        public ShowMonthlyRequestStatsViewModel()
        {
            _currentMonth = "";
            _numberOfRequests = "";
            ExitMonth = new MyICommand(Exit);
        }

        public MyICommand ExitMonth { get; private set; }

        private void Exit()
        {
            new WindowManager().CloseWindow<ShowMonthlyRequestStats>();
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
            get => _numberOfRequests == "0" ? "There weren't any requests!" : _numberOfRequests;
            set
            {
                _numberOfRequests = value;
                OnPropertyChanged();
            }
        }


    }
}
