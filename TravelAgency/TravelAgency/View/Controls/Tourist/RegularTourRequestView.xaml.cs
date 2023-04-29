using System.Windows;
using System.Windows.Controls;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for RegularTourRequestView.xaml
    /// </summary>
    public partial class RegularTourRequestView
    {
        public RegularTourRequestView()
        {
            InitializeComponent();
            DataContext = new RegularTourRequestViewModel();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HomeView());
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
