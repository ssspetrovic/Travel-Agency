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
    }
}
