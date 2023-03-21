﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for GradeGuest.xaml
    /// </summary>
    public partial class GradeGuest : Window
    {
        private readonly GradeGuestViewModel _viewModel = new();
        public GradeGuest()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var OwnerView = new OwnerView();
            OwnerView.Show();
            Close();
        }
    }
}
