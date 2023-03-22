using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using System.Windows;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class ActiveTourRepository : RepositoryBase, IActiveTourRepository
    {
        public void Add(ActiveTour activeTour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            //U bazi se kljucne tacne i turisti cuvaju u vidu teksta, tako da moramo svakog clana respektivnih kolona da povezemo u jedan string
            //Takodje na kraju skracujemo duzinu stringa za 2 kako bi obrisali poslednji zarez

            var allKeyPoints = "";
            var allTourists = "";

            activeTour.KeyPoints[activeTour.KeyPoints.First().Key] = true;

            foreach (var tour in activeTour.KeyPoints)
            {
                allKeyPoints += tour.Key.ToString() + ":" + tour.Value + ", ";
            }

            allKeyPoints = allKeyPoints.Remove(allKeyPoints.Length - 2, 2);

            foreach (var tourist in activeTour.Tourists)
            {
                allTourists += tourist.UserName + ", ";
            }

            allTourists = allTourists.Remove(allTourists.Length - 2, 2);

            const string insertStatement =
                @"insert into ActiveTour(Name, KeyPointsList, Tourists) values ($Name, $KeyPointsList, $Tourists)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);

            insertCommand.Parameters.AddWithValue("$Name", activeTour.Name);
            insertCommand.Parameters.AddWithValue("$KeyPointsList", allKeyPoints);
            insertCommand.Parameters.AddWithValue("$Tourists", allTourists);
            insertCommand.ExecuteNonQuery();
        }

        public void Edit(ActiveTour activeTour)
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            //Posto moze samo jedna tura da bude aktivna, brisemo sve iz tabele
            const string deleteStatement = "delete from ActiveTour";
            using var deleteCommand = new SqliteCommand(deleteStatement, databaseConnection);
            deleteCommand.ExecuteNonQuery();
        }

        public ActiveTour GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsActive()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            return selectReader.Read();
        }

        public string GetActiveTour(string column)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var selectStatement = "select " + column + " from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            return selectReader.Read() ? selectReader.GetString(0) : "Error";
        }

        public void UpdateKeyPoint(string keyPoint)
        {
            var locationRepository = new LocationRepository();
            keyPoint = locationRepository.GetByCity(keyPoint)!.Id.ToString();

            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select KeyPointsList from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var keyPoints = "";

            if (selectReader.Read())
            {
                keyPoints = selectReader.GetString(0);
            }

            databaseConnection.Close();
            databaseConnection.Open();

            //Razdvajamo string kljucnih reci u listu kako bi smo pristupili lokacijama
            var allKeyPoints = keyPoints.Split(", ").ToList();
            var lastKeyPoint = "";

            for (var i = 0; i < allKeyPoints.Capacity; i++)
            {
                var location = allKeyPoints[i];

                if (location.Contains(keyPoint + ":True"))
                {
                    MessageBox.Show("We already passed " + locationRepository.GetByCity(keyPoint)?.City + "!");
                    break;
                }

                //Ako string lokacija:bool sadrzi false i istovremeno pre tog stringa nije bila lokacija lokacija:false onda mozemo da izmenimo bool
                //Jer u suprotnom preskacemo kljucnu tacku sto nema logike u ovom kontekstu
                if (location.Contains(keyPoint + ":False"))
                    if (lastKeyPoint.Contains(":True"))
                    {
                        allKeyPoints[i] = keyPoint + ":True";
                        break;
                    }
                    else
                    {
                        MessageBox.Show("You can't skip key points!");
                        break;
                    }

                lastKeyPoint = allKeyPoints[i];
            }
            
            //Spajamo nazad listu u jedan veci string i menjamo tekst u tabeli
            keyPoints = string.Join(", ", allKeyPoints);

            const string updateStatement = "update ActiveTour set KeyPointsList = $KeyPointsList";
            using var updateCommand = new SqliteCommand(updateStatement, databaseConnection);
            updateCommand.Parameters.AddWithValue("KeyPointsList", keyPoints);
            updateCommand.ExecuteNonQuery();

        }
    }
}
