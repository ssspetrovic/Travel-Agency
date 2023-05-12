﻿using System.Windows.Navigation;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for MyRequestsView.xaml
    /// </summary>
    public partial class MyRequestsView
    {
        public MyRequestsView(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            InitializeComponent();
            DataContext = new MyRequestsViewModel(navigationService, touristViewModel);
        }
    }
}
