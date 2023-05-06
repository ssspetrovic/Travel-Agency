using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using TravelAgency.Service;
using TravelAgency.ViewModel;
using Brushes = System.Windows.Media.Brushes;


namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for AcceptTourRequest.xaml
    /// </summary>
    public partial class AcceptTour
    {
        private readonly AcceptTourViewModel _acceptTourViewModel;
        private readonly TourService _tourService;

        public AcceptTour()
        {
            InitializeComponent();
            _acceptTourViewModel = new AcceptTourViewModel();
            _tourService = new TourService();
        }

        private void ChangeViews_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Oem3)
            {
                var shortcuts = new Shortcuts();
                shortcuts.Closed += Shortcuts_Closed;
                Visibility = Visibility.Collapsed;
                shortcuts.Show();
            }

            if (e.Key == Key.Tab && TourRequestDataGrid.Items.Count > 0)
            {
                e.Handled = true;
                TourRequestDataGrid.SelectedIndex =
                    (TourRequestDataGrid.SelectedIndex + 1) % TourRequestDataGrid.Items.Count;
            }

            if (e.Key == Key.F)
            {
                e.Handled = true;
                FilterRequests_OnClick(sender, e);
            }

            if (e.Key == Key.Enter && TourRequestDataGrid.SelectedItem != null)
                CreateRequestedTour((DataRowView)TourRequestDataGrid.SelectedItem);
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }

        private bool AuthenticateFilters()
        {
            if (_acceptTourViewModel.UpdateView != "Empty") return true;

            _acceptTourViewModel.UpdateView = "";
            _acceptTourViewModel.TourRequestData = _acceptTourViewModel.GetTourRequestData();
            return false;
        }

        private void FilterRequests_OnClick(object sender, RoutedEventArgs e)
        {
            var filterRequests = new FilterRequests();
            filterRequests.Closed += (_, _) =>
            {
                _acceptTourViewModel.UpdateView = filterRequests.UpdateView();
                if (AuthenticateFilters())
                    _acceptTourViewModel.TourRequestData = _acceptTourViewModel.GetTourRequestData();
                TourRequestDataGrid.ItemsSource = _acceptTourViewModel.TourRequestData;
                Visibility = Visibility.Visible;
            };
            Visibility = Visibility.Collapsed;
            filterRequests.Show();
        }

        private void CreateRequestedTour(DataRowView drv)
        {
            var selectRequestedTourDateViewModel = new SelectRequestedTourDateViewModel(drv["DateRange"].ToString()!);
            var selectRequestedTourDate = new SelectRequestedTourDate(selectRequestedTourDateViewModel);
            selectRequestedTourDate.ShowDialog();
            if (!selectRequestedTourDate.GetConformation()) return;

            if (!_tourService.CheckIfDatesExist(selectRequestedTourDate.GetSelectedDates()))
            {
                var createTour = new CreateTour
                {
                    ComboBoxLocation =
                    {
                        Text = drv["Location_Id"].ToString()!.Split(", ")[0],
                        Focusable = false,
                        Background = Brushes.Gray
                    },
                    ComboBoxLanguage =
                    {
                        Text = drv["Language"].ToString(),
                        Focusable = false,
                        Background = Brushes.Gray
                    },
                    MaxGuestsText =
                    {
                        Text = drv["NumberOfGuests"].ToString(),
                        Focusable = false,
                        Background = Brushes.Gray
                    },
                    DateList =
                    {
                        Text = selectRequestedTourDate.GetSelectedDates(),
                    },
                    DatePick =
                    {
                        SelectedDate = null,
                        Focusable = false,
                        Background = Brushes.Gray
                    }
                };

                Content = createTour;
            }
            else
                MessageBox.Show("You already have a tour in the selected date range!");

        }
    }
}
