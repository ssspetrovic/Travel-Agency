using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class AccommodationRepository : RepositoryBase, IAccommodationRepository
    {
        public void Add(Accommodation accommodation)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into Accommodation(name, type, maxGuest, minGuest, locationId, adress, reservableDays, images, description) 
                    values ($name, $type, $maxGuest, $minGuest, $locationId, $adress, $reservableDays, $images, $description)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$name", accommodation.Name);
            insertCommand.Parameters.AddWithValue("$type", accommodation.Type);
            insertCommand.Parameters.AddWithValue("$maxGuest", accommodation.MaxReservationDays);
            insertCommand.Parameters.AddWithValue("$minGuest", accommodation.MinReservationDays);
            insertCommand.Parameters.AddWithValue("$locationId", accommodation.LocationId);
            insertCommand.Parameters.AddWithValue("$adress", accommodation.Adress);
            insertCommand.Parameters.AddWithValue("$reservableDays", accommodation.ReservableDays);
            insertCommand.Parameters.AddWithValue("$images", accommodation.Images);
            insertCommand.Parameters.AddWithValue("$description", accommodation.Description);
            insertCommand.ExecuteNonQuery();
        }
    }
}
