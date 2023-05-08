using System.Windows.Navigation;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for MyToursView.xaml
    /// </summary>
    public partial class MyToursView
    {
        public MyToursView(NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new MyToursViewModel(navigationService);
        }
    }
}