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
using TravelAgency.DTO;
using TravelAgency.Repository;
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
        public ScheduleRenovationView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            AccommodationDTO acc = accommodationRepository.GetByName(cmbAccName.Text);
            DateTime startDate = (DateTime)pickStartDate.SelectedDate;
            DateTime endDate = (DateTime)pickEndDate.SelectedDate;
            int duration = Convert.ToInt32(txtDuration.Text);
        }
    }
}
