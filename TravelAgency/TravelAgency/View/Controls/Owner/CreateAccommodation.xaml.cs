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
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for CreateAccommodation.xaml
    /// </summary>
    public partial class CreateAccommodation : Window
    {
        public CreateAccommodation()
        {
            InitializeComponent();
        }

        private AccommodationType findAccommodationType(string text)
        {
            if (text == "Apartment")
                return AccommodationType.Apartman;
            else if (text == "House")
                return AccommodationType.Kuca;
            else
                return AccommodationType.Cottage;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var accommodationRepository = new AccommodationRepository(); 

            var name = txtName.Text;
            var locationRepository = new LocationRepository();
            var currentLocation = locationRepository.GetByCity(cmbLocation.Text);
            var adress = txtAdress.Text;
            var type = findAccommodationType(cmbType.Text);
            var minDaysReservation = Convert.ToInt32(txtMinReservationDays.Text);
            var maxDaysReservation = Convert.ToInt32(txtMaxReservationDays.Text);
            var reservableDays = Convert.ToInt32(txtReservableDays.Text);
            var description = txtDescription.Text;

            Accommodation accommodation = new Accommodation(name, currentLocation.Id, type, minDaysReservation, maxDaysReservation, adress, reservableDays, 
                ImagesList.Text, description);
            accommodationRepository.Add(accommodation);
            MessageBox.Show("Added Accommodation!");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var OwnerView = new OwnerView();
            OwnerView.Show();
            Close();
        }

        private void txtAddUrl_Click(object sender, RoutedEventArgs e)
        {
            var hasText = false;
            if (ImagesList.Text.Contains(txtImage.Text))
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                ImagesList.Text.Replace(txtImage.Text, "");
                hasText = true;
            }

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            if (ImagesList.Text.Contains(",, "))
                ImagesList.Text.Replace(",, ", ", ");

            if (!hasText)
                if (ImagesList.Text.Length == 0)
                    ImagesList.Text += txtImage.Text;
                else
                    ImagesList.Text += ", " + txtImage.Text;

            if (ImagesList.Text.StartsWith(", "))
                ImagesList.Text = ImagesList.Text.Substring(2, ImagesList.Text.Length - 2);

            txtImage.Text = "";
        }

        private void btnClearImages_Click(object sender, RoutedEventArgs e)
        {
            ImagesList.Text = "";
        }
    }
}