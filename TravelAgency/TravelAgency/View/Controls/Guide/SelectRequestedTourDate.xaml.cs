using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class SelectRequestedTourDate
    {
        private readonly List<string> _selectedDates = new();
        private bool _isConfirmed;

        public SelectRequestedTourDate(SelectRequestedTourDateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void SelectRequestedTourDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ConfirmButton_OnClick(sender, e);
            
            if (e.Key == Key.Escape)
                CancelButton_OnClick(sender, e);

            if ((e.Key is >= Key.F1 and <= Key.F12) || e.SystemKey == Key.F10)
            {
                var index = e.Key - Key.F1;
                var items = DateOptionsItemsControl.Items;
                if (e.SystemKey == Key.F10)
                    index = 9;
                if (index < items.Count)
                {
                    var container = DateOptionsItemsControl.ItemContainerGenerator.ContainerFromItem(items[index]) as ContentPresenter;
                    var checkBox = container!.ContentTemplate.FindName("DateOptionCheckBox", container) as CheckBox;
                    checkBox!.IsChecked = !checkBox.IsChecked;
                }
                e.Handled = true;
            }


        }

        public string GetSelectedDates()
        {
            return string.Join(", ", _selectedDates);
        }

        public bool GetConformation()
        {
            return _isConfirmed;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            _isConfirmed = true;
            Close();
        }

        private void DateOptionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var date = (string)checkbox.Content;
            _selectedDates.Add(date);
        }

        private void DateOptionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var date = (string)checkbox.Content;
            _selectedDates.Remove(date);
        }
    }
}
