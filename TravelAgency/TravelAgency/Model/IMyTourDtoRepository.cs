using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DTO;

namespace TravelAgency.Model
{
    internal interface IMyTourDtoRepository
    {
        void Add(MyTourDto myTour);
        void UpdateStatus(string tourName, MyTourDto.TourStatus newStatus);
        void UpdateKeyPoint(string tourName, string newKeyPoint);
        ObservableCollection<MyTourDto> GetAllAsCollection();
    }
}
