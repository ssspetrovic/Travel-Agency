using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal interface IRenovationRepository
    {
        void Add(Renovation renovation);
        ObservableCollection<Renovation> GetAll();

        ObservableCollection<Renovation> GetAllByAccommodationId(int accommodationId);

        void Remove(int renovationId);
    }
}
