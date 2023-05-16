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
        RenovationRepository renovationRepository = new RenovationRepository();
        public ScheduleRenovationView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (pickStartDate.SelectedDate != null || pickEndDate.SelectedDate != null)
            {
                this.FreeDatesListView.Items.Clear();

                AccommodationDTO acc = accommodationRepository.GetByName(cmbAccName.Text);
                DateTime startDate = (DateTime)pickStartDate.SelectedDate;
                DateTime endDate = (DateTime)pickEndDate.SelectedDate;
                int duration = Convert.ToInt32(txtDuration.Text);

                List<FreeDatesDTO> freeDates = CheckFreeDates(acc.Id, duration, startDate, endDate);

                //string s = "";
                int countFreeDates = 0;
                foreach (FreeDatesDTO fd in freeDates)
                {
                    //s += fd.startDate.ToShortDateString() + " " + fd.endDate.ToShortDateString() + "\n";
                    this.FreeDatesListView.Items.Add(fd);
                    countFreeDates++;
                }
                lblCountFreeDates.Content = "Free: " + countFreeDates.ToString();
                //MessageBox.Show(s);
            }
            else
            {
                MessageBox.Show("You need to select start and end date...", "Message");
            }
        }

        private void btnAppoint_Click(object sender, RoutedEventArgs e)
        {
            if (lblStartDate.Content != null)
            {
                AccommodationDTO acc = accommodationRepository.GetByName(cmbAccName.Text);
                DateTime selectedStartDate = Convert.ToDateTime(lblStartDate.Content);
                DateTime selectedEndDate = Convert.ToDateTime(lblEndDate.Content);
                int duration = Convert.ToInt32(txtDuration.Text);
                string description = txtDescription.Text;

                AppointRenovation(acc.Id, selectedStartDate, selectedEndDate, duration, description);

                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("Successfull appointed renovation!", "Message");
                else
                    MessageBox.Show("Uspešno zakazano renoviranje!", "Poruka");
                Refresh();
            }
            else
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("You need to select a date first...", "Message");
                else
                    MessageBox.Show("Morate prvo datum izabrati...", "Poruka");
            }
        }

        private void Refresh()
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
            ScheduleRenovationView scheduleRenovationView = new ScheduleRenovationView();
            mainFrame.Navigate(scheduleRenovationView);
        }


        private List<FreeDatesDTO> CheckFreeDates(int accommodationId, int duration, DateTime startDate, DateTime endDate)
        {
            List<FreeDatesDTO> freeDates = reservationService.GetFreeDates(accommodationId, duration, startDate, endDate);
            return freeDates;
        }

        private void AppointRenovation(int accommodationId, DateTime selectedStartDate, DateTime selectedEndDate, int duration, string description)
        {
            Renovation renovation = new Renovation(accommodationId, selectedStartDate, selectedEndDate, duration, description);
            renovationRepository.Add(renovation);
        }
    }
}
