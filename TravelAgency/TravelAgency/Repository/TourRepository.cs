using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class TourRepository : RepositoryBase, ITourRepository
    {
        public void Add(Tour tour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            //Za svaku kljucnu tacku saljemo id u tabelu
            var idList = "";
            foreach (var keyPoint in tour.KeyPoints)
            {
                idList += ", " + keyPoint.Id;
            }

            //Brisemo prva dva clana kako bi smo obrisali zarez
            idList = idList.Substring(2, idList.Length - 2);

            const string insertStatement =
                @"insert into Tour(Name, Location_Id, Description, Language, MaxGuests, LocationList, Date, Duration, Images) 
                    values ($Name, $Location_Id, $Description, $Language, $MaxGuests, $LocationList, $Date, $Duration, $Images)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$Name", tour.Name);
            insertCommand.Parameters.AddWithValue("$Location_Id", tour.Location.Id);
            insertCommand.Parameters.AddWithValue("$Description", tour.Description);
            insertCommand.Parameters.AddWithValue("$Language", tour.Language);
            insertCommand.Parameters.AddWithValue("$MaxGuests", tour.MaxGuests);
            insertCommand.Parameters.AddWithValue("$LocationList", idList);
            insertCommand.Parameters.AddWithValue("Date", tour.Date);
            insertCommand.Parameters.AddWithValue("$Duration", tour.Duration);
            insertCommand.Parameters.AddWithValue("$Images", tour.Images);
            insertCommand.ExecuteNonQuery();

        }

        public void Edit(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string deleteStatement = "delete from Tour where Id = $Id";
            using var deleteCommand = new SqliteCommand(deleteStatement, databaseConnection);
            deleteCommand.Parameters.AddWithValue("$Id", id);
            
            deleteCommand.ExecuteNonQuery();
        }

        public Tour GetById(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            Tour? tourModel = null;

            const string selectStatement = "select * from Tour where Id = $Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Id", id);

            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read()) return tourModel!;


            //Posto nam je dosta parametara sacuvano u tabelu u vidu tekstova
            //Moramo da ih konvertujemo u liste podataka
            var locationRepository = new LocationRepository();
            var location = locationRepository.GetById(selectReader.GetInt32(2));
            var keyPointsList = selectReader.GetString(6).Split(", ").ToList();
            var keyPoints = locationRepository.GetByAllCities(keyPointsList);

            tourModel = new Tour(selectReader.GetInt32(0), selectReader.GetString(1),
                location!, selectReader.GetString(3), (Language)selectReader.GetInt32(4), selectReader.GetInt32(5),
                keyPoints!, selectReader.GetString(7), selectReader.GetFloat(8), selectReader.GetString(9));

            return tourModel;
        }

        public Tour GetByName(string? name)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            Tour? tourModel = null;

            const string selectStatement = "select * from Tour where Name = $Name";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Name", name);

            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read()) return tourModel!;

            //Isto i ovde vazi, moramo izvrsiti konverziju
            var locationRepository = new LocationRepository();
            var location = locationRepository.GetById(selectReader.GetInt32(2));
            var keyPointsList = selectReader.GetString(6).Split(", ").ToList();
            var keyPoints = locationRepository.GetByAllCities(keyPointsList);

            tourModel = new Tour(selectReader.GetInt32(0), selectReader.GetString(1),
                location!, selectReader.GetString(3), (Language)selectReader.GetInt32(4), selectReader.GetInt32(5),
                keyPoints!, selectReader.GetString(7), selectReader.GetFloat(8), selectReader.GetString(9));

            return tourModel;
        }

        //Ova funkcija sluzi da bi smo vratili nazad ispis u View
        //Pomocu DataTable dobijamo parametre kolona odnosno dobijamo sve ture
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

        public ObservableCollection<Tour> GetAll()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Tour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var tourList = new ObservableCollection<Tour>();
            var locationRepository = new LocationRepository();

            
            while (selectReader.Read())
            {
                var location = locationRepository.GetById(selectReader.GetInt32(2));
                var keyPointsList = selectReader.GetString(6).Split(", ");
                var keyPoints = locationRepository.GetByAllCities(keyPointsList.ToList());

                tourList.Add(new Tour(selectReader.GetInt32(0), selectReader.GetString(1),
                    location!, selectReader.GetString(3), (Language)selectReader.GetInt32(4), selectReader.GetInt32(5),
                    keyPoints!, selectReader.GetString(7), selectReader.GetFloat(8), selectReader.GetString(9)));
            }

            return tourList;
        }

        //Ova funkcija sluzi u situaciji kada imamo neku turu, ali ta tura ima vise datuma kada se zapocinje
        //Posto ima vise datuma, to znaci da ne smemo celu turu da izbrisemo iz baze podataka vec treba da oznacimo da je danasnja tura gotova
        //Samim tim cemo ovde samo obrisati datum koji se prosledi iz liste svih datuma te ture
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
