using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal interface IReservationRepository
    {
        ObservableCollection<Reservation> GetAll();
        ObservableCollection<Reservation> GetReservationsToGrade();

        ObservableCollection<Reservation> GetGuestsGradesToDisplay();

        int CountReservationsToGrade();
        void UpdateReservationAfterGrading(int reservationId, string comment, float gradeComplacent, float gradeClean);
        void Add(Reservation reservation);

        int CountGradesForOwner(int ownerId);

        double AverageGradeForOwner(int ownerId);

        void AcceptReservationChangeRequest(int reservationId, DateTime newStartDate, DateTime newEndDate);

        //bool IsDelayRequestAvailable(int reservationId, int accommodationId, DateTime newStartDate, DateTime newEndDate);
    }
}
