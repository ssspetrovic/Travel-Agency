using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.Service;
using TravelAgency.View;
using TravelAgency.View.Controls.Guide;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
{
    public class ConfirmDeletionViewModel : HomePageViewModel
    {
        private readonly TourService _tourService;
        private readonly TouristService _touristService;
        private string _deletedTourName;
        private string _selectedDate;

        public ConfirmDeletionViewModel()
        {
            _tourService = new TourService();
            _touristService = new TouristService();
            CancelDeletion = new MyICommand(Cancel);
            _deletedTourName = "";
            _selectedDate = "";
        }

        public MyICommand CancelDeletion { get; private set; }

        public void Cancel()
        {
            new WindowManager().CloseWindow<ConfirmDeletion>();
            var mainWindow = Application.Current.Windows.OfType<Guide>().FirstOrDefault()!.DataContext as GuideViewModel;
            mainWindow!.CurrentViewModel = new CancelTourViewModel();
        }

        public void ConfirmDeletion(string password, string tourName)
        {
            DeletedTourName = tourName;

            if (!AuthenticateDeletion(password) || DeletedTourName == "")
                MessageBox.Show("Tour cancel was not successful");
            else
                CancelTour();

            new WindowManager().CloseWindow<ConfirmDeletion>();
            var mainWindow = Application.Current.Windows.OfType<Guide>().FirstOrDefault()!;
            mainWindow.Title = "Home Page";
            if (mainWindow.DataContext is GuideViewModel guideViewModel)
                guideViewModel.CurrentViewModel = new HomePageViewModel();
        }

        private void CancelTour()
        {
            var tourVoucherService = new TourVoucherService();
            var deletedTour = _tourService.GetByName(DeletedTourName);
            var tourists = _touristService.GetByTour(deletedTour);
            var tourDates = deletedTour.Date.Split(", ").ToList();

            foreach (var tourist in tourists)
                tourVoucherService.Add(new TourVoucher(tourist.Id, tourist.UserName, "Voucher for Cancelled Tour",
                    Convert.ToDateTime(DateTime.Now.AddYears(1).ToString("yyyy-MM-dd")).Date, TourVoucher.VoucherStatus.Valid));

            if (tourDates.Count < 2)
            {
                foreach (var tourist in tourists)
                {
                    _touristService.RemoveTour(tourist.Id);
                    _touristService.UpdateAppearance(tourist.Id, TouristAppearance.Unknown);
                }
                _tourService.Remove(deletedTour.Id);
            }
            else
            {
                var cancelledDate = SelectedDate;
                _tourService.RemoveDate(cancelledDate, tourDates, deletedTour.Id);
            }
        }

        private bool AuthenticateDeletion(string password)
        {
            var userRepository = new UserRepository();
            return password == userRepository.GetByUsername(CurrentUser.Username).Password;
        }


        public string DeletedTourName
        {
            get => _deletedTourName;
            set
            {
                _deletedTourName = value;
                OnPropertyChanged();
            }
        }
        public string SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        public List<RadioButton> InitializeRadioButtonData(ItemsControl itemsControl)
        {
            var dateOptionsList = new List<RadioButton>();
            foreach (var item in itemsControl.Items)
            {
                var container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                if (container?.ContentTemplate.FindName("DateOptions", container) is RadioButton radioButton)
                    dateOptionsList.Add(radioButton);
                
            }

            return dateOptionsList;
        }


        public ObservableCollection<string> Options
        {
            get
            {
                var tourDates = DeletedTourName != "" ? _tourService
                    .GetByName(DeletedTourName)
                    .Date
                    .Split(", ")
                    .Select(dateString => DateTime.ParseExact(dateString, "MM/dd/yyyy", new CultureInfo("en-US"))) : new List<DateTime>();

                var filteredDates = tourDates.Where(date => date >= DateTime.Today.AddHours(48).Date);
                return new ObservableCollection<string>(filteredDates.Select(date => date.ToString("MM/dd/yyyy", new CultureInfo("en-US"))));
            }
        }
    }
}
