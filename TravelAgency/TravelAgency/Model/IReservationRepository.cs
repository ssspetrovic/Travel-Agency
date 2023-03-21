﻿using System;
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
    }
}
