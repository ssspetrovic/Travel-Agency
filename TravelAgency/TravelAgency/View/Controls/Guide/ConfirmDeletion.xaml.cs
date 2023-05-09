using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelAgency.Model;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class ConfirmDeletion
    {
        public ConfirmDeletion()
        {
            InitializeComponent();
            DataContext = new ConfirmDeletionViewModel();
        }

        private void DateChecked(object sender, RoutedEventArgs e)
        {
            CancelledTour.Date = ((RadioButton)sender).Content.ToString();
        }

        private void ConfirmDeletion_OnKeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = DataContext as ConfirmDeletionViewModel;

            if (e.Key == Key.Enter)
            {
                viewModel!.ConfirmDeletion(PasswordBox.Password);
            }

            var dateOptionsList = viewModel!.InitializeRadioButtonData(DateOptionsItemsControl);

            if (e.Key is >= Key.F1 and <= Key.F12)
            {
                var index = e.Key - Key.F1;
                if (index < dateOptionsList.Count)
                {
                    var radioButton = dateOptionsList[index];
                    radioButton.IsChecked = true;
                    e.Handled = true;
                }
            }
        }
    }
}
