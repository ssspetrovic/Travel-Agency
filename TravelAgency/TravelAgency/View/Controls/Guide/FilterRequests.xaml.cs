using System.Windows;
using System.Windows.Input;

namespace TravelAgency.View.Controls.Guide
{
    public partial class FilterRequests
    {
        public FilterRequests()
        {
            InitializeComponent();
        }

        private void FilterRequests_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
                CancelButton_OnClick(sender, e);
            if(e.Key == Key.Enter)
                ConfirmButton_OnClick(sender, e);
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
