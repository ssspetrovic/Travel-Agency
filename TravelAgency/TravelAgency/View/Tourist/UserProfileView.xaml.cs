using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for UserProfileView.xaml
    /// </summary>
    public partial class UserProfileView
    {
        public UserProfileView()
        {
            InitializeComponent();
            DataContext = new UserProfileViewModel();
        }
    }
}
