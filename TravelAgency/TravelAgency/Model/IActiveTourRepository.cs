using System.Data;

namespace TravelAgency.Model
{
    internal interface IActiveTourRepository
    {
        void Add(ActiveTourModel activeTourModel);
        void Edit(ActiveTourModel activeTourModel);
        void Remove();
        ActiveTourModel GetById(int id);
        bool IsActive();
        string GetActiveTourData(string column);
        void UpdateKeyPoint(string keyPoint);
    }
}
