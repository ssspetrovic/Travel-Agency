using System.Windows.Navigation;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for RequestTourView.xaml
    /// </summary>
    public partial class RequestTourView
    {
        public RequestTourView(NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new RequestTourViewModel(navigationService);
        }
    }
}
