using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class TourViewModel
    {
        public Tour Tour { get; set; }
        public string KeyPoints { get; set; }
        //public Uri FirstImageSourceUri { get; set; }
        //public Uri SecondImageSourceUri { get; set; }
        //public Uri ThirdImageSourceUri { get; set; }
        public List<Uri> ParsedUris { get; set; }

        public TourViewModel(Tour tour)
        {
            Tour = tour;
            var tourService = new TourService();

            KeyPoints = tourService.GetFormattedKeyPoints(Tour.KeyPoints);
            ParsedUris = tourService.ParsePhotosUris(Tour.Photos);

            //FirstImageSourceUri = parsedUris.Count > 0 ? parsedUris[0] : new Uri("");
            //SecondImageSourceUri = parsedUris.Count > 1 ? parsedUris[0] : FirstImageSourceUri;
            //ThirdImageSourceUri = parsedUris.Count > 2 ? parsedUris[0] : FirstImageSourceUri;
        }
    }
}
