using System;
using System.Collections.Generic;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class TourViewModel
    {
        public Tour Tour { get; set; }
        public string StartingDate { get; set; }
        public string KeyPoints { get; set; }
        public List<Uri> ParsedUris { get; set; }

        public TourViewModel(Tour tour)
        {
            Tour = tour;
            var tourService = new TourService();

            StartingDate = Tour.Date.Split(", ")[0];
            KeyPoints = tourService.GetFormattedKeyPoints(Tour.KeyPoints);
            ParsedUris = tourService.ParsePhotosUris(Tour.Photos);
        }
    }
}
