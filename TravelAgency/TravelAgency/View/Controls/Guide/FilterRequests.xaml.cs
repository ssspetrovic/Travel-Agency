using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.View.Controls.Guide
{
    public partial class FilterRequests
    {
        private readonly LocationService _locationService;
        private readonly RequestTourService _requestTourService;

        public FilterRequests()
        {
            InitializeComponent();
            _locationService = new LocationService();
            _requestTourService = new RequestTourService();
            LocationBox.Focus();
        }

        public string UpdateView()
        {
            return UpdateViewBox.Text;
        }

        private void FilterRequests_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
                CancelButton_OnClick(sender, e);
            if(e.Key == Key.Enter)
                ConfirmButton_OnClick(sender, e);
            if (e.Key == Key.Tab && DateBox.IsFocused)
            {
                LocationBox.Focus();
                e.Handled = true;
            }
                
        }

        private void CheckTextBoxContent()
        {
            var requestedTours = new List<RequestTour>();

            if (LocationBox.Text.Length > 0)
            {
                var locationIds = _locationService.FindLocationIdByText(LocationBox.Text);
                if (locationIds != "Empty")
                    foreach (var id in locationIds.Split(", "))
                        requestedTours.AddRange(_requestTourService.FindByLocationId(int.Parse(id)));
            }

            if (requestedTours.Count > 0)
            {
                UpdateViewBox.Text = "";
                foreach (var tour in requestedTours)
                    UpdateViewBox.Text += ", " + tour.Id;
                UpdateViewBox.Text = UpdateViewBox.Text.Substring(2, UpdateViewBox.Text.Length - 2);
            }
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            CheckTextBoxContent();
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
