using System.Windows;
using System.Windows.Controls.Primitives;
using TravelAgency.ViewModel;
using TravelAgency.WindowHelpers;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for TouristView.xaml
    /// </summary>
    public partial class TouristView
    {
        public TouristView()
        {
            InitializeComponent();
            HomeButton.IsChecked = true;
            DataContext = new TouristViewModel(new WindowManager(), ContentFrame.NavigationService);
        }

        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
        }
    }
}
