using TravelAgency.Model;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourView
    {
        public TourView(Tour tour)
        {
            InitializeComponent();
            DataContext = new TourViewModel(tour);
        }
    }
}
