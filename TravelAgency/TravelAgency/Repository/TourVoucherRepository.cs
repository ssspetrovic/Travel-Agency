using System;
using System.Collections.ObjectModel;
using TravelAgency.Interface;
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
                    INSERT INTO TourVoucher (TouristId, TouristUsername, Description, ExpirationDate, Status)
                    VALUES ($TouristId, $TouristUsername, $Description, $ExpirationDate, $Status)
                ";
            insertCommand.Parameters.AddWithValue("TouristId", tourVoucher.TouristId);
            insertCommand.Parameters.AddWithValue("$TouristUsername", tourVoucher.TouristUsername);
            insertCommand.Parameters.AddWithValue("Description", tourVoucher.Description);
            insertCommand.Parameters.AddWithValue("ExpirationDate", tourVoucher.ExpirationDate.ToString("yyyy-MM-dd"));
            insertCommand.Parameters.AddWithValue("$Status", tourVoucher.Status);
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

        public void UpdateAllVouchers()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE TourVoucher
                    SET Status = $ExpiredStatus
                    WHERE ExpirationDate < $CurrentDate;
                ";
            updateCommand.Parameters.AddWithValue("$ExpiredStatus", (int)TourVoucher.VoucherStatus.Expired);
            updateCommand.Parameters.AddWithValue("$CurrentDate", DateTime.Today.Date);
            updateCommand.ExecuteNonQuery();
        }

        public void UseVoucher(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE TourVoucher
                    SET Status = $UsedStatus
                    WHERE Id = $id;
                ";
            updateCommand.Parameters.AddWithValue("$UsedStatus", (int)TourVoucher.VoucherStatus.Used);
            updateCommand.Parameters.AddWithValue("$id", id);
            updateCommand.ExecuteNonQuery();
        }

        public ObservableCollection<TourVoucher> GetAllAsCollection()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM TourVoucher WHERE TouristUsername = $CurrentUserUsername";
            selectCommand.Parameters.AddWithValue("$CurrentUserUsername", CurrentUser.Username);
            using var selectReader = selectCommand.ExecuteReader();

            var vouchers = new ObservableCollection<TourVoucher>();
            while (selectReader.Read())
            {
                vouchers.Add(new TourVoucher(
                    selectReader.GetInt32(0),
                    selectReader.GetInt32(1),
                    selectReader.GetString(2),
                    selectReader.GetString(3),
                    selectReader.GetDateTime(4),
                    (TourVoucher.VoucherStatus)selectReader.GetInt32(6)
                ));
            }

            return vouchers;
        }

        public ObservableCollection<TourVoucher> GetAllValidAsCollection()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM TourVoucher WHERE TouristUsername = $CurrentUserUsername AND Status = $ValidStatus";
            selectCommand.Parameters.AddWithValue("$CurrentUserUsername", CurrentUser.Username);
            selectCommand.Parameters.AddWithValue("$ValidStatus", (int)TourVoucher.VoucherStatus.Valid);
            using var selectReader = selectCommand.ExecuteReader();

            var validVouchers = new ObservableCollection<TourVoucher>();
            while (selectReader.Read())
            {
                validVouchers.Add(new TourVoucher(
                    selectReader.GetInt32(0),
                    selectReader.GetInt32(1),
                    selectReader.GetString(2),
                    selectReader.GetString(3),
                    selectReader.GetDateTime(4),
                    (TourVoucher.VoucherStatus)selectReader.GetInt32(6)
                ));
            }

            return validVouchers;
        }


        public TourVoucher GetVoucherByTouristId(int touristId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM TourVoucher WHERE TouristId = $TouristId";
            selectCommand.Parameters.AddWithValue("$TouristId", touristId);
            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read())
                return new TourVoucher(0, touristId, "No user", "No Voucher", DateTime.Today.Date, TourVoucher.VoucherStatus.Expired);

            return new TourVoucher(selectReader.GetInt32(0),
                selectReader.GetInt32(1),
                selectReader.GetString(2),
                selectReader.GetString(3),
                selectReader.GetDateTime(4),
                (TourVoucher.VoucherStatus)selectReader.GetInt32(6));
        }
    }
}
