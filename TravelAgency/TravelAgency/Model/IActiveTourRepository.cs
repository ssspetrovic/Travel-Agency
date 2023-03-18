using System.Data;

namespace TravelAgency.Model
{
    internal interface IActiveTourRepository
    {
        void Add(ActiveTourModel activeTourModel);
        void Edit(ActiveTourModel activeTourModel);
        void Remove(int id);
        ActiveTourModel GetById(int id);
        DataTable GetActiveTour(DataTable dt);
        bool IsActive();
        string GetActiveTourData(string column);
        void RemoveKeyPoint(string keyPoint);
    }
}
