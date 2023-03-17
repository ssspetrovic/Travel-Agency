namespace TravelAgency.Model
{
    internal interface ITouristRepository
    {
        void Add(TouristModel tourModel);
        void Edit(TouristModel tourModel);
        void Remove(int id);
        TouristModel GetById(int id);
        TouristModel GetByName(string? name);
        TouristModel GetByTour(TourModel tour);
    }
}
