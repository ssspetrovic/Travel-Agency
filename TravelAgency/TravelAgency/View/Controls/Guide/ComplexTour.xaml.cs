using System;
using System.Windows;
using System.Windows.Input;

namespace TravelAgency.View.Controls.Guide
{
    public partial class ComplexTour
    {
        public ComplexTour()
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
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }
    }
}
