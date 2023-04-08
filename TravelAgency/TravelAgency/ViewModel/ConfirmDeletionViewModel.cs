using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class ConfirmDeletionViewModel : GuideViewModel
    {
        public readonly TourService TourService;

        public ConfirmDeletionViewModel()
        {
            TourService = new TourService();
        }

        public string DeletedTourName => CancelledTour.Name!;


        public ObservableCollection<string> Options
        {
            get
            {
                var tourDates = TourService
                    .GetByName(CancelledTour.Name)
                    .Date
                    .Split(", ")
                    .Select(DateTime.Parse);

                var filteredDates = tourDates.Where(date => date >= DateTime.Now.AddHours(48));
                return new ObservableCollection<string>(filteredDates.Select(date => date.ToString("MM/dd/yyyy",new CultureInfo("en-US"))));
            }
        }

    }
}
