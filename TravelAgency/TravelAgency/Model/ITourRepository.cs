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
        DataTable GetAllAsDataTable(DataTable dt);
        Language FindLanguage(string language);
        ObservableCollection<Tour> GetAllAsCollection();
        void RemoveDate(string dateToday, List<string> tourDates, int id);
        void UpdateMaxGuests(int id, int maxGuests);
    }
}
