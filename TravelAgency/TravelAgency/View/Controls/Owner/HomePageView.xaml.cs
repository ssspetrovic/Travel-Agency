using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for HomePageView.xaml
    /// </summary>
    public partial class HomePageView : Page
    {
        ReservationRepository reservationRepository = new ReservationRepository();
        public HomePageView()
        {
            InitializeComponent();
            lblName.Content = CurrentUser.DisplayName;

            int gradeCount = reservationRepository.CountGradesForOwner(CurrentUser.Id);
            double averageGrade = reservationRepository.AverageGradeForOwner(CurrentUser.Id);

            lblNumReviews.Content = lblNumReviews.Content + " " + gradeCount.ToString();
            lblGradeAverage.Content = lblGradeAverage.Content + " " + averageGrade.ToString();

            if (gradeCount >= 50 && averageGrade >= 9.5)
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    lblTitle.Content = "Title: Super Owner";
                else
                    lblTitle.Content = "Titula: Super Vlasnik";
            }
            else
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    lblTitle.Content = "Title: Regular Owner";
                else
                    lblTitle.Content = "Titula: Običan Vlasnik";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OwnerMainView mainWindow = null;
            foreach (Window window in Application.Current.Windows)
            {
                if (window is OwnerMainView)
                {
                    mainWindow = (OwnerMainView)window;
                    break;
                }
            }

            Frame mainFrame = mainWindow.mainFrame;
            RegisterAccommodationView registerAccommodationView = new RegisterAccommodationView();
            mainFrame.Navigate(registerAccommodationView);
        }
    }
}
