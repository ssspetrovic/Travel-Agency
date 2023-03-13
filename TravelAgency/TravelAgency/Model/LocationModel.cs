﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class LocationModel
    {
        public int Id { get; set; }
        public string Country { get; set; } 
        public string City {get; set; }

        public LocationModel(int id, string country, string city)
        {
            Id = id;
            Country = country;
            City = city;
        }
    }
}