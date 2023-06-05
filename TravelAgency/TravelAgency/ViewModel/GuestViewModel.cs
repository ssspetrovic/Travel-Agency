using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View;
using TravelAgency.View.Tourist;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
{
    public class GuestViewModel: BaseViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly NavigationService _navigationService;
        public RelayCommand NavigateToHomePageCommand { get; set; }
        public RelayCommand NavigateToMyToursPageCommand { get; set; }
        public RelayCommand NavigateToMyVouchersPageCommand { get; set; }
        public RelayCommand NavigateToMyRequestsCommand { get; set; }
        public RelayCommand NavigateToTourReservationPageCommand { get; set; }
        public RelayCommand NavigateToUserProfileCommand { get; set; }
        public RelayCommand NavigateToRequestTourPageCommand { get; set; }
        public RelayCommand NavigateToNotificationsPageCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }

        public GuestViewModel(NavigationService navigationService)
        {
            _windowManager = new WindowManager();
            _navigationService = navigationService;
        }

    }
}
