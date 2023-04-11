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
using System.Windows.Shapes;
using TravelAgency.Repository;
using TravelAgency.Model;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for SuperOwner.xaml
    /// </summary>
    public partial class SuperOwner : Window
    {
        ReservationRepository reservationRepository = new ReservationRepository();
        public SuperOwner()
        {
            InitializeComponent();
            int gradeCount = reservationRepository.CountGradesForOwner(CurrentUser.Id);
            double averageGrade = reservationRepository.AverageGradeForOwner(CurrentUser.Id);
            lblCount.Content = gradeCount.ToString();
            lblAverage.Content = averageGrade.ToString();

            if (gradeCount >= 50 && averageGrade >= 9.5)
                lblTitle.Content = "Super Owner";
            else
                lblTitle.Content = "Regular Owner";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var OwnerView = new OwnerView();
            OwnerView.Show();
            Close();
        }
    }
}
