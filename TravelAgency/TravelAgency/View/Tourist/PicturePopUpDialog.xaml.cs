using System;
using System.Windows.Controls;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for PicturePopUpDialog.xaml
    /// </summary>
    public partial class PicturePopUpDialog
    {
        public PicturePopUpDialog(Uri imageUri)
        {
            InitializeComponent();
            DataContext = new PicturePopUpDialogViewModel(imageUri);
        }
    }
}
