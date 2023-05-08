using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class HomePage
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = new HomePageViewModel();
        }
    }

}


