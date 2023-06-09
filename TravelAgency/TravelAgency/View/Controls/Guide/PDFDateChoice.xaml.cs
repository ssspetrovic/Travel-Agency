using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class PdfDateChoice
    {
        public PdfDateChoice()
        {
            InitializeComponent();
            DataContext = new PdfDateChoiceViewModel();
        }
    }
}
