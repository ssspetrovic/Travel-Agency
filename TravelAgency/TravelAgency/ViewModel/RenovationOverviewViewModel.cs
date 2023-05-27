using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using TravelAgency.Command;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Controls.Owner;
using TravelAgency.View;
using System.Windows;
using System.Windows.Media;

namespace TravelAgency.ViewModel
{
    public class RenovationOverviewViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _previousRenovationsCollection;
        private readonly CollectionViewSource _futureRenovationsCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;

        public ICollectionView PreviousRenovationsSourceCollection => _previousRenovationsCollection.View;
        public ICollectionView FutureRenovationsSourceCollection => _futureRenovationsCollection.View;

        private RenovationForDisplayDTO? _selectedRenovation;
        private bool _isRenovationSelected;

        public RelayCommand BtnCancel { get; set; }
        private string _lblAccName;
        private string _lblStartDate;
        private string _lblEndDate;

        private readonly RenovationService _renovationService;
        public RenovationOverviewViewModel()
        {
            BtnCancel = new RelayCommand(Execute_BtnCancel);
            _renovationService = new RenovationService();

            _previousRenovationsCollection = new CollectionViewSource
            {
                Source = _renovationService.GetPreviousRenovations()
            };

            _futureRenovationsCollection = new CollectionViewSource
            {
                Source = _renovationService.GetFutureRenovations()
            };
        }

        public bool IsRenovationSelected
        {
            get => _isRenovationSelected;
            set
            {
                _isRenovationSelected = value;
                OnPropertyChanged();
            }
        }
        public RenovationForDisplayDTO? SelectedRenovation
        {
            get => _selectedRenovation;

            set
            {
                _selectedRenovation = value;
                IsRenovationSelected = true;
                OnPropertyChanged();
                LblAccName = _selectedRenovation.AccName;
                LblStartDate = _selectedRenovation.StringStartDate;
                LblEndDate = _selectedRenovation.StringEndDate;
            }
        }

        public string LblAccName
        {
            get => _lblAccName;
            set
            {
                _lblAccName = value;
                OnPropertyChanged();
            }
        }

        public string LblStartDate
        {
            get => _lblStartDate;
            set
            {
                _lblStartDate = value;
                OnPropertyChanged();
            }
        }

        public string LblEndDate
        {
            get => _lblEndDate;
            set
            {
                _lblEndDate = value;
                OnPropertyChanged();
            }
        }

        private void Execute_BtnCancel(object parameter)
        {
            RenovationService _renovationService = new RenovationService();
            if (IsRenovationSelected)
            {
                if (_renovationService.RemoveFutureRenovation(SelectedRenovation.Id, SelectedRenovation.StartDate))
                {
                    MessageBox.Show("Renovation canceled successfully!", "Message");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("Renovation is within 5 days. Cannot cancel..", "Message");
                }
            }
            else
            {
                MessageBox.Show("You need to select a renovation to cancel..", "Message");
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
            RenovationOverviewView renovationOverviewView = new RenovationOverviewView();
            mainFrame.Navigate(renovationOverviewView);
        }
    }
}
