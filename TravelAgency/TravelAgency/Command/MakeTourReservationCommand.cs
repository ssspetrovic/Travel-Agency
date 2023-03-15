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
        private readonly MakeTourReservationViewModel _makeTourReservationViewModel;
        private readonly TourModel _tour;

        public MakeTourReservationCommand(MakeTourReservationViewModel makeTourReservationViewModel, TourModel tour)
        {
            _makeTourReservationViewModel = makeTourReservationViewModel;
            _tour = tour;
        }

        public override void Execute(object? parameter)
        {
            var tourReservation = new TourReservation(_makeTourReservationViewModel.Name,
                _makeTourReservationViewModel.Duration, _makeTourReservationViewModel.Location,
                _makeTourReservationViewModel.Language, _makeTourReservationViewModel.MaxTourists);

            // TODO make reservation
        }
    }
}
