using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace TravelAgency.Model
{
    internal interface ITourRepository
    {
        void Add(TourModel tourModel);
        void Edit(TourModel tourModel);
        void Remove(int id);
        TourModel GetById(int id);
        TourModel? GetByName(string? name);
        DataTable GetByAll(DataTable dt);
        Language FindLanguage(string language);
        ObservableCollection<TourModel> GetAll();
        void RemoveDate(string dateToday, List<string> tourDates, int id);
        void UpdateMaxGuests(int id, int MaxGuests);
    }
}
