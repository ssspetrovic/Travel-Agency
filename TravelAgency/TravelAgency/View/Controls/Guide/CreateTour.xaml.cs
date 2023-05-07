using System.Windows.Input;
using TravelAgency.ViewModel;
using static System.Char;

namespace TravelAgency.View.Controls.Guide
{
    public partial class CreateTour
    {

        public CreateTour()
        {
            InitializeComponent();
            DataContext = new CreateTourViewModel();
        }

        private void DatePick_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsLetterOrDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)))
                DatePick.SelectedDate = null;
            
        }

        private void DatePick_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var viewModel = DataContext as CreateTourViewModel;
            if (e.Key == Key.Enter)
                viewModel?.Dates("Add");
            if (e.Key == Key.Delete)
                viewModel?.Dates("Delete");
        }

        private void GoToTop_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Tab)
                CreateTourScrollViewer.ScrollToTop();
        }
    }
}
