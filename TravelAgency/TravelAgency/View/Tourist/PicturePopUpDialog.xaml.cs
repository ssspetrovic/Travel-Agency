using System;
using TravelAgency.Model;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for PicturePopUpDialog.xaml
    /// </summary>
    public partial class PicturePopUpDialog
    {
        public PicturePopUpDialog(Uri imageSourceUri)
        {
            InitializeComponent();
            DataContext = new PicturePopUpDialogViewModel(imageSourceUri);
        }
    }
}
