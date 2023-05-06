using System.Data;
using System.Diagnostics;
using TravelAgency.Service;
using Key = System.Windows.Input.Key;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using Uri = System.Uri;
using UriKind = System.UriKind;

namespace TravelAgency.View.Controls.Guide
{
    public partial class HomePage
    {
        private readonly TourService _tourService;

        public HomePage()
        {
            InitializeComponent();
            _tourService = new TourService();
        }

        private void ChangeViews_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Tab && GuideViewListView.Items.Count > 0)
            {
                e.Handled = true;
                GuideViewListView.SelectedIndex =
                    (GuideViewListView.SelectedIndex + 1) % GuideViewListView.Items.Count;
            }

            if (e.Key == Key.Enter && GuideViewListView.SelectedItem != null)
            {
                if (e.Handled) return;
                e.Handled = true;

                var selectedItem = (DataRowView)GuideViewListView.SelectedItem;
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

        }

    }
}


