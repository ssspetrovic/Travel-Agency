using System.Windows.Media;
using TravelAgency.View.Controls.Guide;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
{
    public class PopupPicturesViewModel : BaseViewModel
    {


        public PopupPicturesViewModel()
        {
            CloseCommand = new MyICommand(Close);
        }

        private ImageSource? _currentImageSource;

        public ImageSource? CurrentImageSource
        {
            get => _currentImageSource;
            set { _currentImageSource = value; OnPropertyChanged(); }
        }

        public MyICommand CloseCommand { get; private set;  }

        

        private void Close()
        {
            new WindowManager().CloseWindow<PopupPictures>();
        }
	}
}
