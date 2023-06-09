using System.Data;
using System.Windows.Input;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class ComplexTour
    {
        public ComplexTour()
        {
            InitializeComponent();
            DataContext = new ComplexTourViewModel();
        }

        private void ComplexTour_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
                ComplexTourRequestListView.SelectedIndex = (ComplexTourRequestListView.SelectedIndex + 1) % ComplexTourRequestListView.Items.Count;
            if (e.Key == Key.Enter && ComplexTourRequestListView.SelectedItem != null)
            {
                var drv = (DataRowView)ComplexTourRequestListView.SelectedItem;
                var acceptComplexTour = new AcceptComplexTour(drv["RequestedTourIds"].ToString());
                acceptComplexTour.ShowDialog();
            }
        }
    }
}
