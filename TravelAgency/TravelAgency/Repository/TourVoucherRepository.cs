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
                    INSERT INTO TourVoucher (Id, TouristId, Description, ExpireDate)
                    VALUES ($Id, $TouristId, $Description, $ExpireDate)
                ";
            insertCommand.Parameters.AddWithValue("Id", tourVoucher.Id);
            insertCommand.Parameters.AddWithValue("TouristId", tourVoucher.TouristId);
            insertCommand.Parameters.AddWithValue("Description", tourVoucher.Description);
            insertCommand.Parameters.AddWithValue("ExpireDate", tourVoucher.ExpireDate);
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
            throw new NotImplementedException();
        }

        public ObservableCollection<TourVoucher> GetAllAsCollection()
        {
            throw new NotImplementedException();
        }
    }
}
