using Microsoft.Data.Sqlite;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class GuestRepository: RepositoryBase
    {
        public bool IsSuperGuest(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from guest where userId = $Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Id", id);
            using var selectReader = selectCommand.ExecuteReader();

            if (selectReader.Read())
                if (selectReader.GetInt32(5) == 0)
                    return false;
            return true;
        }

        public Guest GetByUserId(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select Id, gradeComplacent, gradeClean, userId, credits, datetime(expDate) from guest where Id = $Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Id", id);
            using var selectReader = selectCommand.ExecuteReader();

            if (selectReader.Read())
                if(selectReader[5] != DBNull.Value)
                {
                    return new Guest(selectReader.GetInt32(0), selectReader.GetInt32(3), selectReader.GetFloat(1), selectReader.GetFloat(2), selectReader.GetInt32(4), selectReader.GetDateTime(5));
                }
                else
                {
                    return new Guest(selectReader.GetInt32(0), selectReader.GetInt32(3), selectReader.GetFloat(1), selectReader.GetFloat(2), selectReader.GetInt32(4));
                }
                    
            return new Guest();
        }

        public void MakeSuperGuest(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var updateCommand = databaseConnection.CreateCommand();
            var updateCommandCredits = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    update Guest set superGuest = 1 where Id = $Id;
                ";
            updateCommandCredits.CommandText =
                @"
                    update Guest set Credits = 5 where Id = $Id;
                ";

            updateCommand.Parameters.AddWithValue("$Id", id);
            updateCommandCredits.Parameters.AddWithValue("$Id", id);

            updateCommand.ExecuteNonQuery();
            updateCommandCredits.ExecuteNonQuery();

        }

        public void ResignSuperGuest(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var updateCommand = databaseConnection.CreateCommand();
            var updateCommandCredits = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    update Guest set superGuest = 0 where Id = $Id;
                ";
            updateCommandCredits.CommandText =
                @"
                    update Guest set Credits = 0 where Id = $Id;
                ";

            updateCommand.Parameters.AddWithValue("$Id", id);
            updateCommandCredits.Parameters.AddWithValue("$Id", id);

            updateCommand.ExecuteNonQuery();
            updateCommandCredits.ExecuteNonQuery();
        }


        public void UpdateCredit(int id, int credit)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    update Guest set credits = $credit where Id = $Id;
                ";


            updateCommand.Parameters.AddWithValue("$Id", id);
            updateCommand.Parameters.AddWithValue("$credit", credit);

            updateCommand.ExecuteNonQuery();

        }
    }
}
