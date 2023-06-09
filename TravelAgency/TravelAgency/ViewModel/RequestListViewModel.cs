using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Data;
using TravelAgency.Service;
using TravelAgency.View.Controls.Guest1;
using TravelAgency.View;
using TravelAgency.Command;
using System.Windows.Navigation;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
{
    public class RequestListViewModel: BaseViewModel, INotifyPropertyChanged
    {
        private readonly CollectionViewSource _requestCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;

        private static DelayRequestService _delayRequestService = new();

        private readonly IWindowManager _windowManager;
        public RelayCommand HomeCommand { get; set; }
        public RelayCommand AccommodationCommand { get; set; }
        public RelayCommand ReservationCommand { get; set; }
        public RelayCommand RequestCommand { get; set; }
        public RelayCommand ProfileCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }

        public RequestListViewModel()
        {
            _windowManager = new WindowManager();

            _requestCollection = new CollectionViewSource
            {
                Source = _delayRequestService.GetAll()
            };

            HomeCommand = new RelayCommand(Execute_NavigateToHomePageCommand);
            AccommodationCommand = new RelayCommand(Execute_NavigateToMyAccommodationsCommand);
            ReservationCommand = new RelayCommand(Execute_NavigateToReservationsCommand);
            RequestCommand = new RelayCommand(Execute_NavigateToRequestsCommand);
            ProfileCommand = new RelayCommand(Execute_NavigateToProfileCommand);
            CloseCommand = new RelayCommand(Execute_CloseWindowCommand);
            SignOutCommand = new RelayCommand(Execute_SignOutCommand);
        }
        public ICollectionView RequestSourceCollection => _requestCollection.View;

        private void Execute_NavigateToHomePageCommand(object parameter)
        {
            _windowManager.ShowWindow<Guest1View>();
            _windowManager.CloseWindow<RequestListView>();
        }

        private void Execute_NavigateToMyAccommodationsCommand(object parameter)
        {
            _windowManager.ShowWindow<AccommodationReservationView>();
            _windowManager.CloseWindow<RequestListView>();
        }

        private void Execute_NavigateToReservationsCommand(object parameter)
        {
            _windowManager.ShowWindow<ReservatoinView>();
            _windowManager.CloseWindow<RequestListView>();
        }

        private void Execute_NavigateToRequestsCommand(object parameter)
        {
            _windowManager.ShowWindow<RequestListView>();
            _windowManager.CloseWindow<RequestListView>();
        }

        private void Execute_NavigateToProfileCommand(object parameter)
        {
            _windowManager.ShowWindow<ProfileView>();
            _windowManager.CloseWindow<RequestListView>();
        }

        private void Execute_CloseWindowCommand(object parameter)
        {
            _windowManager.CloseWindow<RequestListView>();
        }

        private void Execute_SignOutCommand(object parameter)
        {
            _windowManager.ShowWindow<SignInView>();
            _windowManager.CloseWindow<RequestListView>();
        }

    }
}
