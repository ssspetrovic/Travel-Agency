using System;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for CancelTour.xaml
    /// </summary>
    public partial class CancelTour
    {
        private readonly TourService _tourService;

        public CancelTour()
        {
            InitializeComponent();
            _tourService = new TourService();
        }

        private void ChangeViews_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem3)
            {
                var shortcuts = new Shortcuts();
                shortcuts.Closed += Closed;
                Visibility = Visibility.Collapsed;
                shortcuts.Show();
            }

            if (e.Key == Key.Tab && CancelDataGrid.Items.Count > 0)
            {
                e.Handled = true;
                CancelDataGrid.SelectedIndex =
                    (CancelDataGrid.SelectedIndex + 1) % CancelDataGrid.Items.Count;
            }

            if (e.Key == Key.P && CancelDataGrid.SelectedItem != null)
            {
                if (e.Handled) return;
                e.Handled = true;

                var selectedItem = (DataRowView)CancelDataGrid.SelectedItem;
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

            if (e.Key == Key.Enter && CancelDataGrid.SelectedItem != null)
                ConfirmDeletion_OnClick();

        }

        private void Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }


        private void ConfirmDeletion_OnClick()
        {
            var tour = (DataRowView)CancelDataGrid.SelectedItem;
            CancelledTour.Name = _tourService.GetByName(tour["Name"].ToString()).Name;

            var confirmDeletion = new ConfirmDeletion();
            confirmDeletion.Closed += Closed;
            Visibility = Visibility.Collapsed;
            confirmDeletion.Show();
        }
    }
}
