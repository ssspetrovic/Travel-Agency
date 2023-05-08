using System.Windows;

namespace TravelAgency.View.Controls.Guide
{
    public partial class Resign
    {
        public Resign()
        {
            InitializeComponent();
        }

        private void ResignButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            var guideView = new View.Guide();
            guideView.Show();
            Close();
        }
    }
}
