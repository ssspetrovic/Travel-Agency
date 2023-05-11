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
using TravelAgency.Repository;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for GradeGuestsView.xaml
    /// </summary>
    public partial class GradeGuestsView : Page
    {
        private readonly GradeGuestViewModel _viewModel = new();
        private ReservationRepository reservationRepository = new ReservationRepository();
        public GradeGuestsView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void btnGrade_Click(object sender, RoutedEventArgs e)
        {
            if (txtReservationId.Text != "")
            {
                int reservationId = Convert.ToInt32(txtReservationId.Text);
                string comment = txtComment.Text;
                // TODO fix bug
                //float gradeComplaisent = Convert.ToInt32(grid.Children.OfType<RadioButton>().FirstOrDefault(r => r.GroupName == "grade_complaisent" && r.IsChecked.HasValue && r.IsChecked.Value).Content);
                //float gradeClean = Convert.ToInt32(grid.Children.OfType<RadioButton>().FirstOrDefault(r => r.GroupName == "grade_clean" && r.IsChecked.HasValue && r.IsChecked.Value).Content);

                //reservationRepository.UpdateReservationAfterGrading(reservationId, comment, gradeComplaisent, gradeClean);
                MessageBox.Show("Graded!");
            }
            else
                MessageBox.Show("No reservation selected!");
        }
    }
}
