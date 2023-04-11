using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal interface IDelayRequestRepository
    {
        void Add(DelayRequest delayRequest);
        void DeleteById(int id);
    }
}
