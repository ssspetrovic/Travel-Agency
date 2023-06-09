using System;
using System.Collections.Generic;
using System.Windows;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Tourist;

namespace TravelAgency.ViewModel.Tourist
{
    internal class TourViewModel
    {
        public Tour Tour { get; set; }
        public string StartingDate { get; set; }
        public string KeyPoints { get; set; }
        public List<Uri> ParsedUris { get; set; }
        
        public RelayCommand OpenImageCommand { get; set; }

        public TourViewModel(Tour tour)
        {
            Tour = tour;
            var tourService = new TourService();

            StartingDate = Tour.Date.Split(", ")[0];
            KeyPoints = tourService.GetFormattedKeyPoints(Tour.KeyPoints);
            ParsedUris = tourService.ParsePhotosUris(Tour.Photos);

            OpenImageCommand = new RelayCommand(Execute_OpenImageCommand, CanExecute_OpenImageCommand);
        }

        private void Execute_OpenImageCommand(object parameter)
        {
            if (parameter is not Uri imageUri) return;

            var dialog = new PicturePopUpDialog(imageUri)
            {
                Owner = Application.Current.MainWindow
            };

            dialog.Show();
        }

        private bool CanExecute_OpenImageCommand(object parameter)
        {
            return parameter is Uri;
        }
    }
}
