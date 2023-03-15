using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class TourRepository : RepositoryBase, ITourRepository
    {
        public void Add(TourModel tourModel)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var idList = "";
            foreach (var keyPoint in tourModel.KeyPoints)
            {
                idList += "," + keyPoint.Id.ToString();
            }

            idList = idList.Substring(1, idList.Length - 1);

            const string insertStatement =
                @"insert into Tour(Name, Location_Id, Description, Language, MaxGuests, LocationList, Date, Duration, Images) 
                    values ($Name, $Location_Id, $Description, $Language, $MaxGuests, $LocationList, $Date, $Duration, $Images)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$Name", tourModel.Name);
            insertCommand.Parameters.AddWithValue("$Location_Id", tourModel.Location.Id);
            insertCommand.Parameters.AddWithValue("$Description", tourModel.Description);
            insertCommand.Parameters.AddWithValue("$Language", tourModel.Language);
            insertCommand.Parameters.AddWithValue("$MaxGuests", tourModel.MaxGuests);
            insertCommand.Parameters.AddWithValue("$LocationList", idList);
            insertCommand.Parameters.AddWithValue("Date", tourModel.Date);
            insertCommand.Parameters.AddWithValue("$Duration", tourModel.Duration);
            insertCommand.Parameters.AddWithValue("$Images", tourModel.Images);
            insertCommand.ExecuteNonQuery();

        }

        public void Edit(TourModel tourModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public TourModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TourModel GetByName(string? name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TourModel> GetByAll()
        {
            List<TourModel> tourList = new List<TourModel>();
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Tour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            while (selectReader.Read())
            {
                var locationRepository = new LocationRepository();
                var locationList = new List<LocationModel>();

                foreach (var stringId in selectReader["KeyPoints"].ToString().Split(","))
                {
                    var id = int.Parse(stringId);
                    var keyPoint = locationRepository.GetById(id);
                    locationList.Add(keyPoint);

                }

                var location = locationRepository.GetById(selectReader.GetInt32(1));
                tourList.Add(new TourModel(selectReader.GetString(0), location,
                    selectReader.GetString(2),
                    (Language)selectReader.GetInt32(3), selectReader.GetInt32(4), locationList,
                    selectReader.GetString(6), selectReader.GetFloat(7), selectReader.GetString(8)));
            }

            return tourList;
        }

        public Language FindLanguage(string language)
        {
            if (Enum.TryParse(language, out Language result))
            {
                return result switch
                {
                    Language.English => Language.English,
                    Language.French => Language.French,
                    Language.German => Language.German,
                    Language.Spanish => Language.Spanish,
                    Language.Italian => Language.Italian,
                    Language.Japanese => Language.Japanese,
                    Language.Chinese => Language.Chinese,
                    Language.Indian => Language.Indian,
                    Language.Portuguese => Language.Portuguese,
                    _ => Language.Serbian
                };
            }

            return 0;
        }
    }
}
