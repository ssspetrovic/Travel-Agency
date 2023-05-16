using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class RenovationService
    {
        public ObservableCollection<Renovation> GetPreviousRenovations()
        {
            var renovationRepository = new RenovationRepository();
            ObservableCollection<Renovation> previousRenovations = new ObservableCollection<Renovation>();

            ObservableCollection<Renovation> temp = renovationRepository.GetAll();
            foreach(Renovation renovation in temp)
            {
                if(renovation.StartDate <= DateTime.Now)
                {
                    previousRenovations.Add(renovation);
                }
            }
            return previousRenovations;
        }

        public ObservableCollection<Renovation> GetFutureRenovations()
        {
            var renovationRepository = new RenovationRepository();
            ObservableCollection<Renovation> futureRenovations = new ObservableCollection<Renovation>();

            ObservableCollection<Renovation> temp = renovationRepository.GetAll();
            foreach (Renovation renovation in temp)
            {
                if (renovation.StartDate > DateTime.Now)
                {
                    futureRenovations.Add(renovation);
                }
            }
            return futureRenovations;
        }

        public bool RemoveFutureRenovation(int renovationId, DateTime startDate)
        {
            if((startDate - DateTime.Now).TotalDays <= 5)
            {
                return false;
            }

            var renovationRepository = new RenovationRepository();
            renovationRepository.Remove(renovationId);
            return true;
        }

        public bool IsBeingRenovated(int accommodationId, DateTime startDate, DateTime endDate)
        {
            var renovationRepository = new RenovationRepository();
            bool available = true;
            ObservableCollection<Renovation> renovations = renovationRepository.GetAll();

            foreach(Renovation renovation in renovations)
            {
                if((startDate >= renovation.StartDate && startDate <= renovation.EndDate) || (endDate >= renovation.StartDate && endDate <= renovation.EndDate))
                {
                    available = false;
                    break;
                }
            }

            return available;
        }
    }
}
