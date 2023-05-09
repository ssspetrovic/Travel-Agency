using TravelAgency.ViewModel;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for MyTourVouchersView.xaml
    /// </summary>
    public partial class MyTourVouchersView
    {
        public MyTourVouchersView()
        {
            InitializeComponent();
            DataContext = new TourVouchersViewModel();
        }
    }
}
