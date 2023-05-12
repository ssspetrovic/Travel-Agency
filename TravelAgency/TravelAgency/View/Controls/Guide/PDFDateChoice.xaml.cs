using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for PdfDateChoice.xaml
    /// </summary>
    public partial class PdfDateChoice
    {
        public PdfDateChoice()
        {
            InitializeComponent();
            DataContext = new PdfDateChoiceViewModel();
        }
    }
}
