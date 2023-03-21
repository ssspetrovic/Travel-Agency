using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using TravelAgency.DTO;

namespace TravelAgency.Repository
{
    public class AccommodationRepository : RepositoryBase, IAccommodationRepository
    {
        public void Add(Accommodation accommodation)
        {
            throw new NotImplementedException();
        }

        public void Edit(Accommodation accommodation)
        {
            throw new NotImplementedException();
        }

        public Language FindLanguage(string language)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<AccommodationDTO> GetAll()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Accommodation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var accommodationList = new ObservableCollection<AccommodationDTO>();
            var locationRepository = new LocationRepository();


            while (selectReader.Read())
            {
                var location = locationRepository.GetById(selectReader.GetInt32(5));
                var type = Enum.Parse<AccommodationType>(selectReader.GetString(2));

               accommodationList.Add(new AccommodationDTO(selectReader.GetInt32(0), selectReader.GetString(1), location, type, selectReader.GetInt32(3)));
               
            }

            return accommodationList;
        }

        public DataTable GetByAll(DataTable dt)
        {
            throw new NotImplementedException();
        }

        public Accommodation GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Accommodation? GetByName(string? name)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
