using System;
using System.Windows.Controls;
using TravelAgency.Command;
using TravelAgency.View.Tourist;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel.Tourist
{
    internal class PicturePopUpDialogViewModel : BaseViewModel
    {
        public Uri ImageUri { get; set; }

        public RelayCommand CloseCommand { get; set; }

        public PicturePopUpDialogViewModel(Uri imageUri)
        {
            ImageUri = imageUri;
            CloseCommand = new RelayCommand(Execute_CloseCommand);
        }

        private void Execute_CloseCommand(object parameter)
        {
            var windowManager = new WindowManager();
            windowManager.CloseWindow<PicturePopUpDialog>();
        }
    }
}
