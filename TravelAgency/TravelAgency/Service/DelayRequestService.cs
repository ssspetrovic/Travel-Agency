using Microsoft.Data.Sqlite;
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
    public class DelayRequestService
    {
        private static  DelayRequestRepository _repository = new();

        public void Add(DelayRequest delayRequest)
        {
            _repository.Add(delayRequest);
        }

        public ObservableCollection<DelayRequest> GetAll()
        {
            return _repository.GetAll();
        }

        public ObservableCollection<DelayRequestDTO> GetDelayRequests(int ownerId)
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            UserRepository userRepository = new UserRepository();

            ObservableCollection<DelayRequest> delayRequests = _repository.GetDelayRequests(ownerId);
            ObservableCollection<DelayRequestDTO> delayRequestDTO = new ObservableCollection<DelayRequestDTO>();

            foreach(DelayRequest delayRequest in delayRequests)
            {
                AccommodationDTO acc = accommodationRepository.GetById(delayRequest.AccommodationId);
                User u = userRepository.GetById(delayRequest.UserId);
                string guest = u.Name + " " + u.Surname;
                DelayRequestDTO d = new DelayRequestDTO(delayRequest, acc.Name, guest);
                delayRequestDTO.Add(d);
            }

            return delayRequestDTO;
        }
    }
}
