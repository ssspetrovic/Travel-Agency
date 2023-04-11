using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal interface IDelayRequestRepository
    {
        void Add(DelayRequest delayRequest);
        void DeleteById(int id);

        ObservableCollection<DelayRequest> GetDelayRequests(int ownerId);

        void AcceptDelayRequest(int reservationId);

        void RejectDelayRequest(int reservationId, string rejectionComment);
    }
}
