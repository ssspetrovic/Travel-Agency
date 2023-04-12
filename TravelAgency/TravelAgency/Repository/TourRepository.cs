using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Microsoft.VisualBasic;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class TourRepository : RepositoryBase
    {
        public string GetIdList(Tour tour)
        {
            var idList = "";
            foreach (var keyPoint in tour.KeyPoints)
            {
                idList += ", " + keyPoint!.Id;
            }

            idList = idList.Substring(2, idList.Length - 2);

            return idList;
        }

        public void Add(Tour tour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into Tour(Name, Location_Id, Description, Language, MaxGuests, LocationList, Date, Duration, Images) 
                    values ($Name, $Location_Id, $Description, $Language, $MaxGuests, $LocationList, $Date, $Duration, $Images)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$Name", tour.Name);
            insertCommand.Parameters.AddWithValue("$Location_Id", tour.Location.Id);
            insertCommand.Parameters.AddWithValue("$Description", tour.Description);
            insertCommand.Parameters.AddWithValue("$Language", tour.Language);
            insertCommand.Parameters.AddWithValue("$MaxGuests", tour.MaxGuests);
            insertCommand.Parameters.AddWithValue("$LocationList", GetIdList(tour));
            insertCommand.Parameters.AddWithValue("Date", tour.Date);
            insertCommand.Parameters.AddWithValue("$Duration", tour.Duration);
            insertCommand.Parameters.AddWithValue("$Images", tour.Photos);
            insertCommand.ExecuteNonQuery();

        }

        public void Remove(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string deleteStatement = "update Tour set Date = 'DONE' where Id = $Id";
            using var deleteCommand = new SqliteCommand(deleteStatement, databaseConnection);
            deleteCommand.Parameters.AddWithValue("$Id", id);
            
            deleteCommand.ExecuteNonQuery();
        }

        public DataTable GetAllAsDataTable(DataTable dt)
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

        public void RemoveDate(string dateToday, List<string> tourDates, int id)
        {
            var newTourDates = "";
            foreach (var date in tourDates)
            {
                if (!date.Equals(dateToday))
                    newTourDates += ", " + date;
            }

            newTourDates = newTourDates.Substring(2, newTourDates.Length - 2);

            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string updateStatement = "update Tour set Date = $Date where Id = $Id";
            using var updateCommand = new SqliteCommand(updateStatement, databaseConnection);
            updateCommand.Parameters.AddWithValue("$Date", newTourDates);
            updateCommand.Parameters.AddWithValue("$Id", id);

            updateCommand.ExecuteNonQuery();

        }

        public void UpdateMaxGuests(int id, int maxGuests)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText = 
                @"
                    UPDATE Tour SET MaxGuests = $maxGuests
                    WHERE Id = $id;
                ";
            updateCommand.Parameters.AddWithValue("$maxGuests", maxGuests);
            updateCommand.Parameters.AddWithValue("id", id);
            updateCommand.ExecuteNonQuery();
        }
    }
}
