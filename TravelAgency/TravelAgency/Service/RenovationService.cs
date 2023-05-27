using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class RenovationService
    {
        public ObservableCollection<RenovationForDisplayDTO> GetPreviousRenovations()
        {
            var renovationRepository = new RenovationRepository();
            var accommodationRepository = new AccommodationRepository();
            ObservableCollection<RenovationForDisplayDTO> previousRenovations = new ObservableCollection<RenovationForDisplayDTO>();

            ObservableCollection<Renovation> temp = renovationRepository.GetAll();
            foreach(Renovation renovation in temp)
            {
                if(renovation.StartDate <= DateTime.Now)
                {
                    if (renovation.AccommodationId != 0)  //ZBOG BAZE 
                    {
                        AccommodationDTO acc = accommodationRepository.GetById(renovation.AccommodationId);
                        string accName = acc.Name;
                        RenovationForDisplayDTO r = new RenovationForDisplayDTO(renovation, accName);
                        previousRenovations.Add(r);
                    }
                }
            }
            return previousRenovations;
        }

        public ObservableCollection<RenovationForDisplayDTO> GetFutureRenovations()
        {
            var renovationRepository = new RenovationRepository();
            var accommodationRepository = new AccommodationRepository();
            ObservableCollection<RenovationForDisplayDTO> futureRenovations = new ObservableCollection<RenovationForDisplayDTO>();

            ObservableCollection<Renovation> temp = renovationRepository.GetAll();
            foreach (Renovation renovation in temp)
            {
                if (renovation.StartDate > DateTime.Now)
                {
                    if (renovation.AccommodationId != 0)
                    {
                        AccommodationDTO acc = accommodationRepository.GetById(renovation.AccommodationId);
                        string accName = acc.Name;
                        RenovationForDisplayDTO r = new RenovationForDisplayDTO(renovation, accName);
                        futureRenovations.Add(r);
                    }
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

        public bool IsRenovated(int accommodationId)
        {
            var renovationRepository = new RenovationRepository();
            ObservableCollection<Renovation> renovations = renovationRepository.GetAll();

            foreach(Renovation renovation in renovations)
            {
                if(renovation.AccommodationId == accommodationId)
                {
                    if(renovation.EndDate <= DateTime.Now && (DateTime.Now - renovation.EndDate).TotalDays <= 365)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
