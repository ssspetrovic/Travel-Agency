﻿using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for MyTours.xaml
    /// </summary>
    public partial class MyToursView
    {
        public MyToursView2()
        {
            InitializeComponent();
            DataContext = new MyToursViewModel();
        }

        private void SignOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TourReservationButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToursButton_OnClick(object sender, RoutedEventArgs e)
        {
            var myToursView = new MyToursView();
            myToursView.Show();
            Close();
        }
        private void MyVouchersButton_OnClick(object sender, RoutedEventArgs e)
        {
            var myVouchersView = new TourVouchers();
            myVouchersView.Show();
            Close();
        }

        private void JoinTourButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((MyToursViewModel)DataContext).MyTourDtoService.JoinTour();
        }

        private void RateTourButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((MyToursViewModel)DataContext).RateTour();
        }
    }
}