using System;
using System.IO;
using TravelAgency.View.Controls.Guide;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
{
    public class VideoTutorialViewModel : HomePageViewModel
    {
        private string _url;

        public VideoTutorialViewModel()
        {
            _url = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\Videos\"));
            CloseCommand = new MyICommand(OnClose);
        }

        public MyICommand CloseCommand { get; private set; }

        private void OnClose()
        {
            new WindowManager().CloseWindow<VideoTutorial>();
        }

        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }
    }
}
