using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Controls.Tourist;
using static System.Windows.Application;

namespace TravelAgency.ViewModel
{
    internal class MyToursViewModel : BaseViewModel
    {
        private MyTourDto? _selectedTour;

        public MyTourDtoService MyTourDtoService { get; }
        public string? InvitationText { get; set; }
        public MyTourDto? SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MyTourDto> MyTours { get; set; }


        public MyToursViewModel()
        {
            MyTourDtoService = new MyTourDtoService(this);
            CheckForInvitation();
            MyTours = MyTourDtoService.GetAllAsCollection();
        }

        private void CheckForInvitation()
        {
            var touristService = new TouristService();
            var tourist = touristService.GetByUsername(CurrentUser.Username);

            if (tourist.TouristAppearance != TouristAppearance.Pinged) return;
            var tourName = tourist.Tour.Name;
            InvitationText = $"Guide is asking you to check in for '{tourName}' tour!";

            if (!IsAttendanceConfirmed()) return;
            touristService.UpdateAppearanceByUsername(CurrentUser.Username, TouristAppearance.Present);
            MyTourDtoService.UpdateStatus(tourName, MyTourDto.TourStatus.Attending);

        }
        private bool IsAttendanceConfirmed()
        {
            var dialog = new AcceptInvitationDialog()
            {
                Owner = Current.MainWindow,
                DataContext = this
            };

            dialog.ShowDialog();
            return AcceptInvitationDialog.ConfirmStatus;
        }

        public static void ReloadWindow()
        {
            Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = new MyToursView();
                var currentWindow = Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                mainWindow.Show();
                currentWindow?.Close();
            });
        }
    }
}
