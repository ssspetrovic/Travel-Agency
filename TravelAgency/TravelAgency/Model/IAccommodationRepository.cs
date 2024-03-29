﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DTO;

namespace TravelAgency.Model
{
    internal interface IAccommodationRepository
    {
        void Add(Accommodation accommodation);
        void Edit(Accommodation accommodation);
        void RemoveByName(string accName);
        AccommodationDTO GetById(int id);
        AccommodationDTO GetByName(string name);
        DataTable GetByAll(DataTable dt);
        Language FindLanguage(string language);
        ObservableCollection<AccommodationDTO> GetAll();
        ObservableCollection<AccommodationDTO> GetAllByOwnerId();

        int GetOwnerIdByAccommodationId(int accommodationId);

    }
}
