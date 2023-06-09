using System.Windows.Navigation;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestView.xaml
    /// </summary>
    public partial class ComplexTourRequestView
    {
        public ComplexTourRequestView(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            InitializeComponent();
            DataContext = new ComplexTourRequestViewModel(navigationService, touristViewModel);
        }
    }
}
