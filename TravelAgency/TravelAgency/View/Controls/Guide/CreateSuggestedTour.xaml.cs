using System.Windows.Input;
using System.Windows;
using System;
using System.Windows.Media;

namespace TravelAgency.View.Controls.Guide
{
    public partial class CreateSuggestedTour
    {
        public CreateSuggestedTour()
        {
            InitializeComponent();
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

            if (e.Key == Key.A)
                CreateByLocation_OnClick(sender, e);

            if (e.Key == Key.B)
                CreateByLanguage_OnClick(sender, e);
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }

        private void CreateByLocation_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var createTour = new CreateTour
            {
                ComboBoxLocation =
                {
                    Text = LocationText.Text.Split(", ")[0],
                    Focusable = false,
                    Background = Brushes.Gray
                },
                KeyPointsList =
                {
                    Text = LocationText.Text.Split(", ")[0]
                }
            };
            Content = createTour;
        }

        private void CreateByLanguage_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var createTour = new CreateTour
            {
                ComboBoxLanguage =
                {
                    Text = LanguageText.Text,
                    Focusable = false,
                    Background = Brushes.Gray
                }
            };

            Content = createTour;
        }
    }
}
