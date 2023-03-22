using System.Data;

namespace TravelAgency.Model
{
    internal interface IActiveTourRepository
    {
        void Add(ActiveTour activeTour);
        void Edit(ActiveTour activeTour);
        void Remove();
        ActiveTour GetById(int id);
        bool IsActive();
        string GetActiveTour(string column);
        void UpdateKeyPoint(string currentKeyPoint);
    }
}
