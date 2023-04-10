using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
                    INSERT INTO TourVoucher (TouristId, Description, ExpirationDate)
                    VALUES ($TouristId, $Description, $ExpirationDate)
                ";

            insertCommand.Parameters.AddWithValue("TouristId", tourVoucher.TouristId);
            insertCommand.Parameters.AddWithValue("Description", tourVoucher.Description);
            insertCommand.Parameters.AddWithValue("ExpirationDate", tourVoucher.ExpirationDate.ToString("d/M/yyyy"));
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
            selectCommand.CommandText = "SELECT * FROM TourVoucher WHERE TouristId = $CurrentUserId";
            selectCommand.Parameters.AddWithValue("$CurrentUserId", CurrentUser.Id);
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

        public TourVoucher GetVoucherByTourist(int touristId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM TourVoucher WHERE TouristId = $TouristId";
            selectCommand.Parameters.AddWithValue("$TouristId", touristId);
            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read())
                return new TourVoucher(0, touristId, "No Voucher", DateTime.Now);

            return new TourVoucher(selectReader.GetInt32(0),
                selectReader.GetInt32(1),
                selectReader.GetString(2),
                selectReader.GetDateTime(3));
        }
    }
}
