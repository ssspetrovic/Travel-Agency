using System;
using System.Windows;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class ReviewTour
    {

        public ReviewTour()
        {
            InitializeComponent();
            DataContext = new ReviewTourViewModel();
        }

        private void CreateTourCopy_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
