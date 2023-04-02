namespace TravelAgency.Model
{
    internal interface IFinishedTourRepository
    {
        void Add(FinishedTour finishedTour);
        void Edit(FinishedTour finishedTour);
        void Remove();
        FinishedTour GetById(int id);
    }
}
