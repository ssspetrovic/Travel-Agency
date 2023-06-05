using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelAgency.View.Controls.Guest1;
using TravelAgency.View.Tourist;
using TravelAgency.ViewModel;
using System.Windows.Controls.Primitives;


namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for Guest1View.xaml
    /// </summary>
    public partial class Guest1View : Window
    {
        public Guest1View()
        {
            InitializeComponent();
            HomeButton.IsChecked = true;
            DataContext = new GuestViewModel(ContentFrame.NavigationService);
        }

        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }

        
    }
}
