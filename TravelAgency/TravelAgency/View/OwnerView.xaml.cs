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
using System.Windows.Shapes;
using TravelAgency.View.Controls;
using TravelAgency.View.Controls.Owner;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OwnerView : Window
    {
        public OwnerView()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private void btnCreateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            var createAccommodationView = new CreateAccommodation();
            createAccommodationView.Show();
            Close();   
        }
    }
}
