using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel
{
    internal class TourReservationViewModel : BaseViewModel
    {
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;

        public TourReservationViewModel()
        {
            _locationRepository = new LocationRepository();
            _tourRepository = new TourRepository();
        }

        public DataView Tours
        {
            get
            {
                var dataTable = new DataTable();
                dataTable = _tourRepository.GetByAll(dataTable);

                ConvertColumn(dataTable, "Location_Id", typeof(string), "Location");
                ConvertColumn(dataTable, "Language", typeof(string), "Language");
                ConvertColumn(dataTable, "LocationList", typeof(string), "KeyPoints");

                return dataTable.DefaultView;
            }
        }

        public void ConvertColumn(DataTable dataTable, string columnName, Type newType, string tourParameter)
        {
            var textList = new List<string?>();

            foreach (DataRow row in dataTable.Rows)
            {
                switch (tourParameter)
                {
                    case "Language":
                        textList.Add(Enum.GetName(typeof(Language), row["Language"]));
                        break;
                    case "Location":
                        var locationId = Convert.ToInt32(row["Location_Id"]);
                        textList.Add(_locationRepository.GetById(locationId)?.City + ", " + _locationRepository.GetById(locationId)?.Country);
                        break;
                    case "KeyPoints":
                        var keyPointsString = "";
                        foreach (var id in row[columnName].ToString()?.Split(", ")!)
                        {
                            var keyPointId = Convert.ToInt32(id);
                            keyPointsString += "; " + _locationRepository.GetById(keyPointId)?.City + ", " + _locationRepository.GetById(keyPointId)?.Country;
                        }

                        keyPointsString = keyPointsString.Substring(2, keyPointsString.Length - 2);
                        textList.Add(keyPointsString);
                        break;
                }
            }

            foreach (DataRow row in dataTable.Rows)
            {
                row[columnName] = Convert.ChangeType(row[columnName], newType);
            }

            dataTable.Columns.Remove(columnName);
            var dataColumn = new DataColumn(columnName, newType);
            dataTable.Columns.Add(dataColumn);
            dataColumn.SetOrdinal(dataTable.Columns[columnName]!.Ordinal);

            var i = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                row[columnName] = textList[i++];
            }
        }
    }
}
