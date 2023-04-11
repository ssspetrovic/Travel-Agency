using System;
using System.Windows;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for AcceptInvitationDialog.xaml
    /// </summary>
    public partial class AcceptInvitationDialog
    {
        public static bool ConfirmStatus { get; private set; }

        public AcceptInvitationDialog()
        {
            InitializeComponent();
            ConfirmStatus = false;
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            ConfirmStatus = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            ConfirmStatus = false;
            Close();
        }
    }
}
