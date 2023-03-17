using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Data.Sqlite;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class TouristRepository : RepositoryBase, ITouristRepository
    {
        public void Add(TouristModel tourModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(TouristModel tourModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public TouristModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TouristModel GetByName(string? name)
        {
            throw new NotImplementedException();
        }

        public List<TouristModel> GetByTour(TourModel tour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var tourists = new List<TouristModel>();

            const string selectStatement = "select * from Tourist where Tour_Id = $Tour_Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Tour_Id", tour.Id);

            using var selectReader = selectCommand.ExecuteReader();
            while (selectReader.Read())
                tourists.Add(new TouristModel(selectReader.GetString(1), selectReader.GetString(2), selectReader.GetString(3), selectReader.GetString(4), selectReader.GetString(5), (Role) selectReader.GetInt32(6), tour, TouristCheck.Unchecked));
            

            return tourists;
        }
    }
}
