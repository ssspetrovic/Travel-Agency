using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for RequestsStatisticsView.xaml
    /// </summary>
    public partial class RequestsStatisticsView
    {
        public RequestsStatisticsView()
        {
            InitializeComponent();
            DataContext = new RequestsStatisticsViewModel();
        }
    }
}
