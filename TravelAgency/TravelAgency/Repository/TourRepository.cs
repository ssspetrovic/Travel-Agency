using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
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
                idList += ", " + keyPoint.Id.ToString();
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
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            TourModel? tourModel = null;

            const string selectStatement = "select * from Tour where Name = $Name";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Name", name);

            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read()) return tourModel!;


            var locationRepository = new LocationRepository();
            var location = locationRepository.GetById(selectReader.GetInt32(2));
            var keyPointsList = selectReader.GetString(6).Split(", ").ToList();
            var keyPoints = locationRepository.GetByAllCities(keyPointsList);

            tourModel = new TourModel(selectReader.GetInt32(0), selectReader.GetString(1),
                location!, selectReader.GetString(3), (Language)selectReader.GetInt32(4), selectReader.GetInt32(5),
                keyPoints!, selectReader.GetString(7), selectReader.GetFloat(8), selectReader.GetString(9));

            return tourModel;
        }

        public DataTable GetByAll(DataTable dt)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from Tour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            dt.Load(selectCommand.ExecuteReader());
            return dt;
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

        public ObservableCollection<TourModel> GetAll()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Tour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var tourList = new ObservableCollection<TourModel>();
            var locationRepository = new LocationRepository();


            while (selectReader.Read())
            {
                var location = locationRepository.GetById(selectReader.GetInt32(2));
                var keyPointsList = selectReader.GetString(6).Split(", ");
                var keyPoints = locationRepository.GetByAllCities(keyPointsList.ToList());

                tourList.Add(new TourModel(selectReader.GetInt32(0), selectReader.GetString(1),
                    location!, selectReader.GetString(3), (Language)selectReader.GetInt32(4), selectReader.GetInt32(5),
                    keyPoints!, selectReader.GetString(7), selectReader.GetFloat(8), selectReader.GetString(9)));
            }

            return tourList;
        }
    }
}
