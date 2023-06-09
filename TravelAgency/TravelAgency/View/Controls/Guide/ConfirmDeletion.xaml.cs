using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class ConfirmDeletion
    {
        private readonly ConfirmDeletionViewModel _confirmDeletionViewModel;

        public ConfirmDeletion()
        {
            InitializeComponent();
            _confirmDeletionViewModel = new ConfirmDeletionViewModel();
            DataContext = _confirmDeletionViewModel;
        }

        private void DateChecked(object sender, RoutedEventArgs e)
        {
            _confirmDeletionViewModel.SelectedDate = ((RadioButton)sender).Content.ToString()!;
        }

        private void ConfirmDeletion_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _confirmDeletionViewModel.ConfirmDeletion(PasswordBox.Password, TourNameText.Text);
            }

            var dateOptionsList = _confirmDeletionViewModel.InitializeRadioButtonData(DateOptionsItemsControl);

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
