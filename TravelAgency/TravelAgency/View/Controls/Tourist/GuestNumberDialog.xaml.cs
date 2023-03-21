﻿using System.Windows;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for GuestNumberDialog.xaml
    /// </summary>
    public partial class GuestNumberDialog
    {
        public static bool IsUpdateConfirmed { get; set; }

        public GuestNumberDialog()
        {
            InitializeComponent();
            IsUpdateConfirmed = false;
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            IsUpdateConfirmed = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            IsUpdateConfirmed = false;
            Close();
        }
    }
}