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
    /// Interaction logic for AccommodationStatisticsView.xaml
    /// </summary>
    public partial class AccommodationStatisticsView : Page
    {
        AccommodationRepository accommodationRepository = new AccommodationRepository();
        AccommodationService accommodationService = new AccommodationService();
        private readonly AccommodationStatisticsViewModel _viewModel = new();
        public AccommodationStatisticsView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void cmbAccName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAccName.Text != "")
            {
                this.StatsByYearListView.Items.Clear();

                AccommodationDTO accommodation = accommodationRepository.GetByName(cmbAccName.SelectedItem.ToString());
                ObservableCollection<AccommodationStatDTO> statList = accommodationService.GetAccommodationStatByYear(accommodation.Id);
                foreach (AccommodationStatDTO a in statList)
                {
                    this.StatsByYearListView.Items.Add(a);
                }
            }
        }
    }
}
