using System;
using TravelAgency.Command;
using TravelAgency.View.Tourist;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel.Tourist
{
    internal class PicturePopUpDialogViewModel : BaseViewModel
    {
        public RelayCommand CloseCommand { get; set; }

        private Uri _imageSourceUri;

        public Uri ImageSourceUri
        {
            get => _imageSourceUri;
            set
            {
                _imageSourceUri = value;
                OnPropertyChanged();
            }
        } 

        public PicturePopUpDialogViewModel(Uri imageSourceUri)
        {
            _imageSourceUri = imageSourceUri;
            CloseCommand = new RelayCommand(Execute_CloseCommand);
        }

        private void Execute_CloseCommand(object parameter)
        {
            var windowManager = new WindowManager();
            windowManager.CloseWindow<PicturePopUpDialog>();
        }
    }
}
