using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using TravelAgency.Model;

namespace TravelAgency.Interface
{
    public interface IRequestTourRepository
    {
        DataTable GetAllAsDataTable(DataTable dt, bool isComplex);
        DataTable GetAllComplexAsDataTable(DataTable dt);
        string GetMostRequestedStat(string column);
        DataTable UpdateDataTable(DataTable dt, string ids);
        DataTable UpdateComplexDataTable(DataTable dt, string ids);
        List<string> GetAllRequestedLanguages();
        void Add(RequestTour requestTour);
        ObservableCollection<RequestTour> GetAllForSelectedYearAsCollection(string? year = null, string? touristUsername = null);
        ObservableCollection<string> GetAllYearsAsCollection();
        void UpdateStatusById(int id, Status newStatus);
        RequestTour GetById(int id);
        void UpdateAcceptedDateById(int id, string acceptedDate);
        string GetRequestedTourIdsById(int id);
        ObservableCollection<RequestTour> GetAllAsCollectionByComplexId(string? touristUsername, int complexId = -1);
    }
}
