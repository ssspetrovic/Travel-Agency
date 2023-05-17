using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TravelAgency.Model;
using TravelAgency.Service;
using static System.Text.RegularExpressions.Regex;

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
            if (e.Key == Key.Tab && DatePick.IsFocused)
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
                        requestedTours.AddRange(_requestTourService.FindByParameter(id, "Location_Id"));
            }

            if (GuestsBox.Text.Length > 0)
            {
                var foundTours = _requestTourService.FindByParameter(GuestsBox.Text, "MaxGuests");
                if (requestedTours.Count == 0)
                    requestedTours = foundTours.ToList();
                else
                    requestedTours = (from tour in requestedTours join found in foundTours
                            on tour.Id equals found.Id select tour).ToList();
            }

            if (LanguageBox.Text.Length > 0)
            {
                var foundTours = _requestTourService.FindByParameter(LanguageBox.Text, "Language");
                if (requestedTours.Count == 0)
                    requestedTours = foundTours.ToList();
                else
                    requestedTours = (from tour in requestedTours join found in foundTours
                            on tour.Id equals found.Id select tour).ToList();
            }

            if (DatePick.Text.Length > 0)
            {
                var foundTours = _requestTourService.FindByParameter(DatePick.Text, "DateRange");
                if (requestedTours.Count == 0)
                    requestedTours = foundTours.ToList();
                else
                    requestedTours = (from tour in requestedTours join found in foundTours
                            on tour.Id equals found.Id select tour).ToList();
            }

            if (requestedTours.Count == 0)
            {
                MessageBox.Show("Filter found no results!");
                return;
            }
            UpdateViewBox.Text = "";
            foreach (var tour in requestedTours)
                UpdateViewBox.Text += ", " + tour.Id;
            UpdateViewBox.Text = UpdateViewBox.Text.Substring(2, UpdateViewBox.Text.Length - 2);
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

        private void GuestsBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsMatch(e.Text, @"^[0-9]+$"))
            {
                TextFieldAssist.SetUnderlineBrush(GuestsBox, Brushes.Red);
                e.Handled = true;
            }
            else
            {
                var brushConverter = new BrushConverter();
                var brush = (Brush)brushConverter.ConvertFromString("#4fc3f7")!;
                TextFieldAssist.SetUnderlineBrush(GuestsBox, brush);
            }
        }

        private void DatePick_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ConfirmButton_OnClick(sender, e);
                e.Handled = true;
            }
        }
    }
}
