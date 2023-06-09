using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using TravelAgency.Service;
using TravelAgency.View.Controls.Guide;

namespace TravelAgency.ViewModel
{
    public class CancelTourViewModel : HomePageViewModel
    {
        private readonly TourService _tourService;

        public CancelTourViewModel()
        {
            _tourService = new TourService();
            CancelTourCommands = new MyICommand<string>(CancelCommands);
        }

        public MyICommand<string> CancelTourCommands { get; private set; }

        public void CancelCommands(string commands)
        {
            switch (commands)
            {
                case "KeyPPressed":
                    ParseLinks();
                    break;
                case "EnterPressed":
                    var cancelledTour = SelectedTour != null ? _tourService.GetByName(SelectedTour!["Name"].ToString()).Name : _tourService.GetByName(CancelTours[0]["Name"].ToString()).Name;
                    var confirmDeletion = new ConfirmDeletion();
                    confirmDeletion.DataContext = new ConfirmDeletionViewModel
                    {
                        DeletedTourName = cancelledTour
                    };
                    confirmDeletion.PasswordBox.Focus();
                    confirmDeletion.ShowDialog();
                    OnPropertyChanged(nameof(CancelTours));
                    break;
            }
        }

        public DataView CancelTours
        {
            get
            {
                var dt = new DataTable();
                dt = _tourService.GetAllAsDataTable(dt);

                var indexList = new List<int>();

                for (var index = 0; index < dt.Rows.Count; index++)
                {
                    var row = dt.Rows[index];
                    var dates = row["Date"].ToString()!.Split(", ");
                    indexList.Add(index);

                    foreach (var date in dates)
                    {
                        if (date == "DONE") break;
                        var convertedDate = DateTime.Parse(date, new CultureInfo("en-US"));
                        var twoDaysAgo = DateTime.Today.AddDays(2);
                        if (convertedDate < twoDaysAgo) continue;
                        indexList.Remove(index);
                        break;
                    }
                }

                for (var i = indexList.Count - 1; i >= 0; i--)
                {
                    dt.Rows.RemoveAt(indexList[i]);
                }

                ConvertTourColumn(dt, "Location_Id", typeof(string), "Location");
                ConvertTourColumn(dt, "Language", typeof(string), "Language");
                ConvertTourColumn(dt, "LocationList", typeof(string), "KeyPoints");

                return dt.DefaultView;
            }
        }
    }
}
