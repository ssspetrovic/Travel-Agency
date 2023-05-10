using System;
using System.Windows;
using System.Windows.Input;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class VideoTutorial
    {
        private bool _isPaused;

        public VideoTutorial()
        {
            InitializeComponent();
            DataContext = new VideoTutorialViewModel();
            VideoPlayer.Play();
            VideoPlayer.Volume = 0.3;
            VideoPlayer.IsMuted = true;
            MuteTextBlock.Visibility = Visibility.Visible;
        }

        private void VideoTutorial_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key is Key.Enter or Key.Space)
            {
                _isPaused = !_isPaused;
                if (_isPaused)
                    VideoPlayer.Pause();
                else
                    VideoPlayer.Play();
            }

            if (e.Key == Key.Escape)
            {
                Close();
            }

            if (e.Key == Key.Left)
            {
                if (VideoPlayer.Position.TotalSeconds > 5)
                {
                    VideoPlayer.Position -= TimeSpan.FromSeconds(5);
                }
                else
                {
                    VideoPlayer.Position = TimeSpan.Zero;
                }
            }

            if (e.Key == Key.Right)
            {
                if (VideoPlayer.Position.TotalSeconds < VideoPlayer.NaturalDuration.TimeSpan.TotalSeconds - 5)
                {
                    VideoPlayer.Position += TimeSpan.FromSeconds(5);
                }
                else
                {
                    VideoPlayer.Position = VideoPlayer.NaturalDuration.TimeSpan;
                }
            }

            if (e.Key == Key.M)
            {
                VideoPlayer.IsMuted = !VideoPlayer.IsMuted;
                MuteTextBlock.Visibility = VideoPlayer.IsMuted ? Visibility.Visible : Visibility.Hidden;

            }

        }

        private void VideoPlayer_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Position = TimeSpan.Zero;
            //VideoPlayer.Play();
        }
    }
}
