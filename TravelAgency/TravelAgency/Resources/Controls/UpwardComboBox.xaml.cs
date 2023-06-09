using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace TravelAgency.Resources.Controls
{
    public partial class UpwardComboBox
    {
        private bool _isPopupAdjusted;

        public UpwardComboBox()
        {
            Loaded += UpwardComboBox_Loaded;
        }

        private void UpwardComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (Template.FindName("toggleButton", this) is not ToggleButton toggleButton) return;
            toggleButton.Click += ToggleButton_Click;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsDropDownOpen)
            {
                AdjustPopupPlacement();
            }
        }

        protected override void OnDropDownOpened(EventArgs e)
        {
            base.OnDropDownOpened(e);
            AdjustPopupPlacement();
        }

        private void AdjustPopupPlacement()
        {
            if (_isPopupAdjusted) return;
            if (Template.FindName("PART_Popup", this) is not Popup dropDownPopup) return;
            
            dropDownPopup.Placement = PlacementMode.Top;
            dropDownPopup.HorizontalOffset += 5;
            dropDownPopup.VerticalOffset += 5;
            _isPopupAdjusted = true;
        }
    }
}