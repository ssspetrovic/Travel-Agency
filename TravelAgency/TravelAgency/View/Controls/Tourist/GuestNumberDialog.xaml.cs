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

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for GuestNumberDialog.xaml
    /// </summary>
    public partial class GuestNumberDialog : Window
    {
        public static bool IsUpdateConfirmed { get; set; }

        public GuestNumberDialog()
        {
            InitializeComponent();
            IsUpdateConfirmed = false;
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            IsUpdateConfirmed = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            IsUpdateConfirmed = false;
            Close();
        }
    }
}
