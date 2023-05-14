using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelAgency.Service;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for RenovationOverviewView.xaml
    /// </summary>
    public partial class RenovationOverviewView : Page
    {
        private readonly RenovationOverviewViewModel _viewModel = new();
        RenovationService renovationService = new RenovationService();
        public RenovationOverviewView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void FutureRenovationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnCancelRenovation_Click(object sender, RoutedEventArgs e)
        {
            if (txtRenovationID.Text != "")
            {
                int renovationID = Convert.ToInt32(txtRenovationID.Text);
                DateTime startDate = Convert.ToDateTime(lblStartDate.Content);
                if (renovationService.RemoveFutureRenovation(renovationID, startDate))
                {
                    MessageBox.Show("Renovation canceled successfully!", "Message");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("Renovation is within 5 days. Cannot cancel..", "Message");
                }
            }
            else
            {
                MessageBox.Show("You need to select a renovation to cancel..", "Message");
            }
        }

        private void Refresh()
        {
            OwnerMainView mainWindow = null;
            foreach (Window window in Application.Current.Windows)
            {
                if (window is OwnerMainView)
                {
                    mainWindow = (OwnerMainView)window;
                    break;
                }
            }

            Frame mainFrame = mainWindow.mainFrame;
            RenovationOverviewView renovationOverviewView = new RenovationOverviewView();
            mainFrame.Navigate(renovationOverviewView);
        }
    }
}
