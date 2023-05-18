using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for WizardDialogView.xaml
    /// </summary>
    public partial class WizardDialogView
    {
        public WizardDialogView()
        {
            InitializeComponent();
            DataContext = new WizardDialogViewModel();
        }
    }
}
