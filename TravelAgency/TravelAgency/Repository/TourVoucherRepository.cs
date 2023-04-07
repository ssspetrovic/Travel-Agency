using System;
using System.Collections.ObjectModel;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class TourVoucherRepository : RepositoryBase, ITourVoucherRepository
    {
        public void Add(TourVoucher tourVoucher)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var insertCommand = databaseConnection.CreateCommand();
            insertCommand.CommandText =
                @"
                    INSERT INTO TourVoucher (Id, TouristId, Description, ExpirationDate)
                    VALUES ($Id, $TouristId, $Description, $ExpirationDate)
                ";
            insertCommand.Parameters.AddWithValue("Id", tourVoucher.Id);
            insertCommand.Parameters.AddWithValue("TouristId", tourVoucher.TouristId);
            insertCommand.Parameters.AddWithValue("Description", tourVoucher.Description);
            insertCommand.Parameters.AddWithValue("ExpirationDate", tourVoucher.ExpirationDate);
            insertCommand.ExecuteNonQuery();
        }

        public void DeleteById(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var deleteCommand = databaseConnection.CreateCommand();
            deleteCommand.CommandText = "DELETE FROM TourVoucher WHERE Id = $Id";
            deleteCommand.Parameters.AddWithValue("$Id", id);
            deleteCommand.ExecuteNonQuery();
        }

        public void DeleteExpired()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var deleteCommand = databaseConnection.CreateCommand();
            deleteCommand.CommandText = "DELETE FROM TourVoucher WHERE $ExpirationDate > ExpirationDate";
            deleteCommand.Parameters.AddWithValue("$ExpirationDate", DateTime.Now);
            deleteCommand.ExecuteNonQuery();
        }

        public ObservableCollection<TourVoucher> GetAllAsCollection()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM TourVouchers";
            using var selectReader = selectCommand.ExecuteReader();

            var vouchers = new ObservableCollection<TourVoucher>();
            while (selectReader.Read())
            {
                vouchers.Add(new TourVoucher(
                    selectReader.GetInt32(0),
                    selectReader.GetInt32(1),
                    selectReader.GetString(2),
                    selectReader.GetDateTime(3)
                ));
            }

            return vouchers;
        }
    }
}
