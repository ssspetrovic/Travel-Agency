using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class ForumService
    {
        ForumRepository forumRepository = new ForumRepository();
        public ForumService() { }

        public ObservableCollection<Forum> GetForumsForOwner()
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            ObservableCollection<AccommodationDTO> accommodations = accommodationRepository.GetAllByOwnerId();

            ObservableCollection<Forum> forums = forumRepository.GetAll();
            ObservableCollection<Forum> forumsForOwner = new ObservableCollection<Forum>();
            foreach(Forum forum in forums)
            {
                foreach(AccommodationDTO acc in accommodations)
                {
                    if(acc.Location.Id == forum.LocationId)
                    {
                        forumsForOwner.Add(forum);
                        break;
                    }
                }
            }
            return forumsForOwner;
        }

        public int GetCountForumNotVisited()
        {
            ObservableCollection<Forum> forums = GetForumsForOwner();
            int count = 0; 
            foreach(Forum forum in forums)
            {
                if (forum.VisitedByOwner == 0)
                    count++;
            }
            return count;
        }

        public ObservableCollection<ForumDTO> DisplayForums()
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            ObservableCollection<AccommodationDTO> accommodations = accommodationRepository.GetAllByOwnerId();

            ObservableCollection<Forum> forums = forumRepository.GetAll();
            ObservableCollection<ForumDTO> forumsForOwner = new ObservableCollection<ForumDTO>();
            foreach (Forum forum in forums)
            {
                foreach (AccommodationDTO acc in accommodations)
                {
                    if (acc.Location.Id == forum.LocationId)
                    {
                        ForumDTO forumDTO = new ForumDTO();
                        forumDTO.Id = forum.Id;
                        forumDTO.Comments = forum.OwnerCommentNumber + forum.GuestCommentNumber;

                        UserRepository userRepository = new UserRepository();
                        User u = userRepository.GetById(forum.GuestId);
                        forumDTO.GuestName = u.Name + " " + u.Surname;

                        if (forum.IsClosed)
                            forumDTO.Closed = "Closed";
                        else
                            forumDTO.Closed = "";

                        if (forum.VisitedByOwner == 0)
                            forumDTO.IsVisited = "NEW";
                        else
                            forumDTO.IsVisited = "";

                        LocationRepository locationRepository = new LocationRepository();
                        Location l = locationRepository.GetById(forum.LocationId);
                        forumDTO.Location = l.Country + ", " + l.City;

                        forumsForOwner.Add(forumDTO);
                        break;
                    }
                }
            }
            return forumsForOwner;
        }
    }
}
