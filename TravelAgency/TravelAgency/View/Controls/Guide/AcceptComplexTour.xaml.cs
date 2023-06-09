using System.Windows.Input;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class AcceptComplexTour
    {
        public AcceptComplexTour(string? requestedIds)
        {
            InitializeComponent();
            DataContext = new AcceptComplexTourViewModel(requestedIds);
        }

        private void AcceptComplexTour_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
