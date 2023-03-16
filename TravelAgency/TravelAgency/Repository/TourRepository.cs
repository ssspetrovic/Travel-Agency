using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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
            throw new NotImplementedException();
        }

        public DataTable GetByAll(DataTable dt)
        {
            var tourList = new List<TourModel>();
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Tour";
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
    }
}
