using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Documents;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class TourViewModel
    {
        public Tour Tour { get; set; }
        public string KeyPoints { get; set; }

        public TourViewModel(Tour tour)
        {
            Tour = tour;
            KeyPoints = Tour.KeyPoints.ToString()!;
        }

        private void GetFormattedKeyPoints(List<Location?> keyPoints)
        {

        }
    }
}
