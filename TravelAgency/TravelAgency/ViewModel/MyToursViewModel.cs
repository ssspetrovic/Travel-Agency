using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using TravelAgency.DTO;
using TravelAgency.Service;
using TravelAgency.View.Controls.Tourist;
using static System.Windows.Application;

namespace TravelAgency.ViewModel
{
    internal class MyToursViewModel : BaseViewModel
    {
        public MyTourDtoService MyTourDtoService { get; set; }
        
        private MyTourDto? _selectedTour;

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
            MyTours = MyTourDtoService.GetAllAsCollection();
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
