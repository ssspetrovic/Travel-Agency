using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace TravelAgency.ViewModel
{
    public class SelectRequestedTourDateViewModel : HomePageViewModel
    {
        public SelectRequestedTourDateViewModel()
        {
            _dateRange = "";
        }

        public SelectRequestedTourDateViewModel(string dateRange)
        {
            _dateRange = dateRange;
        }

        private string _dateRange;

        public ObservableCollection<string> DateOptions
        {
            get
            {
                var filteredDates = new ObservableCollection<string>();
                if (DateRange != "")
                {
                    var dateRangeParts = DateRange.Split(" - ");
                    var startDate = DateTime.ParseExact(dateRangeParts[0], "MM/dd/yyyy", new CultureInfo("en-US"));
                    var endDate = DateTime.ParseExact(dateRangeParts[1], "MM/dd/yyyy", new CultureInfo("en-US"));

                    var currentDate = startDate;
                    while (currentDate <= endDate)
                    {
                        filteredDates.Add(currentDate.ToString("MM/dd/yyyy", new CultureInfo("en-US")));
                        currentDate = currentDate.AddDays(1);
                    }

                }

                return filteredDates.Count == 0 ? new ObservableCollection<string> {"Empty", "Empty"} : filteredDates;
            }
        }

        public string DateRange
        {
            get => _dateRange;
            set
            {
                if (_dateRange == value) return;
                _dateRange = value;
                OnPropertyChanged();
            }
        }
    }
}
