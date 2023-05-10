using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class PopupPictures
    {
        public PopupPictures()
        {
            InitializeComponent();
            DataContext = new PopupPicturesViewModel();
        }
    }
}
