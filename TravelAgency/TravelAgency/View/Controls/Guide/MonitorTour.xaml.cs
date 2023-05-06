using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;


namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for MonitorTour.xaml
    /// </summary>
    
    public partial class MonitorTour
    {
        private readonly ActiveTourService _activeTourService;
        private readonly TouristService _touristService;
        private readonly TourService _tourService;

        public MonitorTour()
        {
            InitializeComponent();
            _activeTourService = new ActiveTourService();
            _touristService = new TouristService();
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

            if (e.Key == Key.Tab && MonitorDataGrid.Items.Count > 0)
            {
                e.Handled = true;
                MonitorDataGrid.SelectedIndex =
                    (MonitorDataGrid.SelectedIndex + 1) % MonitorDataGrid.Items.Count;
            }

            if (e.Key == Key.P && MonitorDataGrid.SelectedItem != null)
            {
                if (e.Handled) return;
                e.Handled = true;

                var selectedItem = (DataRowView)MonitorDataGrid.SelectedItem;
                var images = _tourService.GetByName(selectedItem["Name"].ToString()!).Photos;
                var links = images.Split(", ");
                foreach (var link in links)
                {
                    if (Uri.TryCreate(link, UriKind.Absolute, out var uriResult) &&
                        (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = uriResult.AbsoluteUri,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show($"Invalid link: {link}");
                    }
                }
            }

            if (e.Key == Key.Enter && MonitorDataGrid.SelectedItem != null)
                TourIsActive_OnClick(false);

            if (e.Key == Key.A)
                CurrentActiveTour_OnClick(sender, e);
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }

        private void TourIsActive_OnClick(bool needsUpdate)
        {

            if (!_activeTourService.IsActive() || needsUpdate)
            {
                Tour selectedTour;
                if (_activeTourService.IsActive())
                    selectedTour = _tourService.GetByName(_activeTourService.GetActiveTourColumn("Name"));
                else
                {
                    var drv = (DataRowView)MonitorDataGrid.SelectedItem;
                    selectedTour = _tourService.GetByName(drv["Name"].ToString());
                }

                var tourists = _touristService.GetByTour(selectedTour);

                var activeKeyPoints = selectedTour.KeyPoints.ToDictionary(location => location!.Id, _ => false);

                var currentKeyPoint = selectedTour.Location;
                _activeTourService.Remove();
                _activeTourService.Add(new ActiveTour(selectedTour.Name, activeKeyPoints, tourists, currentKeyPoint.City));

                var myTourDtoService = new MyTourDtoService();
                myTourDtoService.UpdateStatus(selectedTour.Name, MyTourDto.TourStatus.Active);
                myTourDtoService.UpdateKeyPoint(selectedTour.Name, currentKeyPoint.ToString());
            }

            else
                MessageBox.Show("You already have an active tour!");
            
            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            var currentWindow = Window.GetWindow(this);
            currentWindow?.Close();
        }

        private void CurrentActiveTour_OnClick(object sender, RoutedEventArgs e)
        {

            if (_activeTourService.IsActive())
            {
                TourIsActive_OnClick(true);
            }
            else
                MessageBox.Show("There is no active tour at the moment!");
        }

    }
}
