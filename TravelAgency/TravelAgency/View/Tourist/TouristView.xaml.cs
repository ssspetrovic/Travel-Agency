using System.Windows.Controls.Primitives;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for TouristView.xaml
    /// </summary>
    public partial class TouristView
    {
        public TouristView()
        {
            InitializeComponent();
            HomeButton.IsChecked = true;
            DataContext = new TouristViewModel(ContentFrame.NavigationService);
        }
    }
}
