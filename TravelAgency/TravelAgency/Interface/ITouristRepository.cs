using System.Collections.Generic;
using TravelAgency.Model;

namespace TravelAgency.Interface
{
    public interface ITouristRepository
    {
        List<Tourist> GetAllDto();
        List<Tourist> GetByTour(Tour tour);
        void RemoveTour(int id);
        void UpdateAppearance(int id, TouristAppearance appearance);
        void UpdateAppearanceByUsername(string? username, TouristAppearance appearance);
        TouristAppearance GetTouristAppearance(string? username);
        void JoinTour(string? username, int tourId, int locationId);
        int GetCompletedToursCountByUsername(string? username);
        void UpdateToursCount(string? username, int newCount);
    }
}
