using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class Shortcuts
    {

        public Shortcuts()
        {
            InitializeComponent();
            DataContext = new ShortcutsViewModel();
        }
    }

}
