using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using TravelAgency.Model;
using TravelAgency.WindowHelpers;
using TravelAgency.Command;
using TravelAgency.DTO;
using TravelAgency.View.Controls.Guest1;
using TravelAgency.Repository;
using TravelAgency.Service;
using System.CodeDom;

namespace TravelAgency.ViewModel
{
    public class SingleAccommodationViewModel: BaseViewModel
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly IWindowManager _windowManager;
        private readonly NavigationService _navigationService;

        private AccommodationDTO _accommodation;
        private DateTime startDate;
        private DateTime endDate;
        private int gradeClean;
        private string guestNumber;
        private int gradeOwner;

        public RelayCommand RelayCommand { get; set; }

        public SingleAccommodationViewModel(NavigationService navigationService, HomeViewModel viewModel) {
            startDate = DateTime.Now;
            endDate = DateTime.Now;
            _homeViewModel = viewModel;
            _navigationService = navigationService;
            _windowManager = new WindowManager();
            _accommodation = viewModel.SelectedAccommodation;
            gradeOwner = 3;
            gradeClean = 2;
        }

        public AccommodationDTO Accommodation
        {
            get => _accommodation;
            set
            {
                _accommodation = value;
            }
        }

        public string GuestNumber
        {
            get => guestNumber;
            set
            {
                guestNumber = value;
            }
        }

        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
            }
        }
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
            }
        }

        public int GradeClean
        {
            get => gradeClean;
            set
            {
                gradeClean = value;
            }
        }

        public int GradeOwner
        {
            get => gradeOwner;
            set
            {
                gradeOwner = value;
            }
        }

        public void Exacute_ReserveCommand(object parameter)
        {
            var _reservationService = new ReservationService();
            _reservationService.Reserve(EndDate, StartDate, Accommodation, int.Parse(this.GuestNumber));

            
        }
    }
}
