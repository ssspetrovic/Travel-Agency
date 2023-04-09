﻿using System.Collections.Generic;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class TourRatingService : RepositoryBase
    {
        private readonly TourRatingRepository _tourRatingRepository;

        public TourRatingService()
        {
            _tourRatingRepository = new TourRatingRepository();
        }

        public List<string> GetCommentsByTourId(int id)
        {
            return _tourRatingRepository.GetCommentsByTourId(id);
        }

        public List<string> GetRatingsByTourId(int id)
        {
            return _tourRatingRepository.GetRatingsByTourId(id);
        }

        public void ReportAComment(string comment)
        {
            _tourRatingRepository.ReportAComment(comment);
        }
    }
}