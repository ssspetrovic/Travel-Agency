using System.Windows.Controls.Primitives;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for TouristView.xaml
    /// </summary>
    public partial class TouristView
    {
        public TouristView(NextView nextView = NextView.Home)
        {
            InitializeComponent();
            HomeButton.IsChecked = true;
            DataContext = new TouristViewModel(ContentFrame.NavigationService, nextView);
        }

        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
        }
    }
}
