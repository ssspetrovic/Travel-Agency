using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class TourReservationRepository : RepositoryBase, ITourReservationRepository
    {
        public void Add(TourReservation tourReservation)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var insertCommand = databaseConnection.CreateCommand();
            insertCommand.CommandText =
                @"
                    INSERT INTO TourReservation (TourId, Username, Name)
                    VALUES ($TourId, $Username, $Name)
                ";
            insertCommand.Parameters.AddWithValue("TourId", tourReservation.TourId);
            insertCommand.Parameters.AddWithValue("Username", tourReservation.Username);
            insertCommand.Parameters.AddWithValue("Name", tourReservation.DisplayName);
            insertCommand.ExecuteNonQuery();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public TourReservation GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
