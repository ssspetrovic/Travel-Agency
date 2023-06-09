using System.Windows;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class HomePage
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = new HomePageViewModel();

            if (int.Parse(FinishedToursCounter.Text.Split(": ")[1]) >= 20 && float.Parse(AverageRating.Text.Split(": ")[1]) >= 4.5)
                IsSuperGuide.Visibility = Visibility.Visible;
            else
                IsSuperGuide.Visibility = Visibility.Hidden;

        }
    }

}


