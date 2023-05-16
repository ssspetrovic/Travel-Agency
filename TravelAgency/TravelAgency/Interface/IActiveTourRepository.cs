using TravelAgency.Model;

namespace TravelAgency.Interface;

public interface IActiveTourRepository
{
    string GetAllKeyPoints(ActiveTour activeTour);
    string GetAllTourists(ActiveTour activeTour);
    void Add(ActiveTour activeTour);
    void Remove();
    bool IsActive();
    bool ExistsByName(string name);
    string GetCurrentKeyPointByName(string name);
}