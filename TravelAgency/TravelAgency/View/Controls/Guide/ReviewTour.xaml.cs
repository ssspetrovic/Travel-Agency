using System;
using System.Windows;
using System.Windows.Input;
using TravelAgency.Service;

namespace TravelAgency.View.Controls.Guide
{
    public partial class ReviewTour
    {
        private readonly TourRatingService _tourRatingService;

        public ReviewTour()
        {
            InitializeComponent();
            _tourRatingService = new TourRatingService();
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

            if (e.Key == Key.Tab && ListViewComments.Items.Count > 0)
            {
                e.Handled = true;
                ListViewComments.SelectedIndex =
                    (ListViewComments.SelectedIndex + 1) % ListViewComments.Items.Count;
            }

            if (e.Key == Key.Enter && (ListViewComments.SelectedItem != null || ReportButton.IsFocused))
            {
                if (ReportButton.IsFocused)
                {
                    ReportComment_OnClick(sender, e);
                }
                else
                {
                    var selectedIndex = ListViewComments.SelectedIndex;
                    ReportedCommentTxt.Text = ListViewComments.SelectedItem!.ToString()!;
                    ReportedTouristTxt.Text = ListViewTourists.Items[selectedIndex].ToString()!;
                    ReportButton.Focus();
                }
            }
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }

        private void CreateTourCopy_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ReportComment_OnClick(object sender, RoutedEventArgs e)
        {
            if (ReportedCommentTxt.Text.Length == 0)
                MessageBox.Show("You haven't a comment to report!");
            else
            {
                _tourRatingService.ReportValidation(ReportedCommentTxt.Text, ReportedTouristTxt.Text);
                this.InvalidateVisual();
            }

        }

    }
}
