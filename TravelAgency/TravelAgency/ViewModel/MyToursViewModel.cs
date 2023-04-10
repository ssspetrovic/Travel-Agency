using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using TravelAgency.DTO;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    internal class MyToursViewModel : BaseViewModel
    {
        public MyTourDtoService MyTourDtoService { get; set; }
        
        private MyTourDto? _selectedTour;
        public ICollectionView MyToursView { get; set; }

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
            MyToursView = CollectionViewSource.GetDefaultView(MyTours);
        }
    }
}
