using System.Windows;
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
            NavigationService?.GoBack();
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((RegularTourRequestViewModel)DataContext).SubmitTourRequest();
        }
    }
}
