using System.Windows.Controls.Primitives;
using TravelAgency.ViewModel;

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
            DataContext = new TouristViewModel(ContentFrame.NavigationService);
        }

        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
        }
    }
}
