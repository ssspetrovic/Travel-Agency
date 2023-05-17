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

            const string selectStatement = "select Id, gradeComplacent, gradeClean, userId, credits, expDate from guest where Id = $Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Id", id);
            using var selectReader = selectCommand.ExecuteReader();
            
            if (selectReader.Read())
                    return new Guest(selectReader.GetInt32(0), selectReader.GetInt32(3), selectReader.GetFloat(1), selectReader.GetFloat(2), selectReader.GetInt32(4), selectReader.GetInt32(5));
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
    }
}
