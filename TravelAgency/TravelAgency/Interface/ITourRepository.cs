using System.Collections.Generic;
using System.Data;
using TravelAgency.Model;

namespace TravelAgency.Interface
{
    public interface ITourRepository
    {
        string GetIdList(Tour tour);
        void Remove(int id);
        DataTable GetAllAsDataTable(DataTable dt);
        Language FindLanguage(string language);
        void RemoveDate(string dateToday, List<string> tourDates, int id);
        void UpdateMaxGuests(int id, int maxGuests);
        bool CheckIfDatesExist(string dates);
    }
}
