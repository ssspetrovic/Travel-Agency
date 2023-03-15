using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using TravelAgency.ViewModel;

namespace TravelAgency.Command
{
    internal class MakeTourReservationCommand : BaseCommand
    {
        private readonly TourModel _tour;

        public MakeTourReservationCommand(MakeTourReservationViewModel makeTourReservationViewModel, TourModel tour)
        {
            _tour = tour;
        }


        public override void Execute(object? parameter)
        {

        }
    }
}
