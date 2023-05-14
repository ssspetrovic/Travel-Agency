using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.Service;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for ScheduleRenovationView.xaml
    /// </summary>
    public partial class ScheduleRenovationView : Page
    {
        private readonly ScheduleRenovationViewModel _viewModel = new();
        AccommodationRepository accommodationRepository = new AccommodationRepository();
        ReservationService reservationService = new ReservationService();
        public ScheduleRenovationView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            this.FreeDatesListView.Items.Clear();

            AccommodationDTO acc = accommodationRepository.GetByName(cmbAccName.Text);
            DateTime startDate = (DateTime)pickStartDate.SelectedDate;
            DateTime endDate = (DateTime)pickEndDate.SelectedDate;
            int duration = Convert.ToInt32(txtDuration.Text);

            List<FreeDatesDTO> freeDates = reservationService.GetFreeDates(acc.Id, duration, startDate, endDate);

            string s = "";
            int countFreeDates = 0;
            foreach (FreeDatesDTO fd in freeDates)
            {
                s += fd.startDate.ToShortDateString() + " " + fd.endDate.ToShortDateString() + "\n";
                this.FreeDatesListView.Items.Add(fd);
                countFreeDates++;
            }
            lblCountFreeDates.Content = "Free: " + countFreeDates.ToString();
            MessageBox.Show(s);
        }
    }
}
