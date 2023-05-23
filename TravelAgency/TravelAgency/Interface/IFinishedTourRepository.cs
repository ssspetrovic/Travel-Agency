using System.Collections.ObjectModel;
using System.Data;
using LiveCharts;
using TravelAgency.Model;

namespace TravelAgency.Interface
{
    public interface IFinishedTourRepository
    {
        string GetAllKeyPoints(Tour finishedTour);
        string GetAllTourists(Tour finishedTour);
        void Add(Tour finishedTour);
        void Edit(Tour finishedTour);
        bool CheckExistingTours(Tour tour);
        ObservableCollection<Tour> FindBestTourByYear(ObservableCollection<Tour> allFinishedTours, string year);
        ChartValues<int> GetAgeGroup(Tour finishedTour);
        DataTable GetAllAsDataTable(DataTable dt);
        string GetNewTourName();
        float GetAverageRating(string username);
        int GetNumberOfToursByUsername(string username);
    }
}
