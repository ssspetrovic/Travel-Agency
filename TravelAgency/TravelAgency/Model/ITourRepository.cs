using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace TravelAgency.Model
{
    internal interface ITourRepository
    {
        void Add(Tour tour);
        void Edit(Tour tour);
        void Remove(int id);
        Tour GetById(int id);
        Tour? GetByName(string? name);
        DataTable GetByAll(DataTable dt);
        Language FindLanguage(string language);
        ObservableCollection<Tour> GetAll();
        void RemoveDate(string dateToday, List<string> tourDates, int id);
        void UpdateMaxGuests(int id, int maxGuests);
    }
}
