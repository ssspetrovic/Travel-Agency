﻿using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel
{
    public class MonitorTourViewModel : GuideViewModel
    {
        private readonly TourRepository _tourRepository;


        public MonitorTourViewModel()
        {
            _tourRepository = new TourRepository();
        }

        public DataView ToursToday
        {
            get
            {
                var dt = new DataTable();
                dt = _tourRepository.GetAllAsDataTable(dt);

                var indexList = new List<int>();

                for (var index = 0; index < dt.Rows.Count; index++)
                {
                    var row = dt.Rows[index];

                    if (!row["Date"].ToString()!.Contains(DateTime.Now.ToString("MM/dd/yyyy", new CultureInfo("en-US"))))
                        indexList.Add(index);
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
