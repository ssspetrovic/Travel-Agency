using System.Windows;
using System.Windows.Controls;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for RequestTourView.xaml
    /// </summary>
    public partial class RequestTourView : Page
    {
        public RequestTourView()
        {
            InitializeComponent();
        }

        private void RegularTourRequestButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegularTourRequestView());
        }
    }
}
