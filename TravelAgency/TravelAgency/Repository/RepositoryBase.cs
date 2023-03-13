using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace TravelAgency.Repository
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            _connectionString = "Server=(local); Database=MVVMLoginDb; Integrated Security = true";
        }

        protected SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString);
        }
    }
}
