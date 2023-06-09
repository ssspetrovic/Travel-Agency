using TravelAgency.Interface;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    internal class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly TourService _tourService;

        public UserService()
        {
            _userRepository = new UserRepository();
            _tourService = new TourService();
        }

        public User GetByUsername(string? username)
        {
            return _userRepository.GetByUsername(username);
        }

        public void RemoveByUsername(string? username)
        {
            _userRepository.RemoveByUsername(username);
        }

        public void SetSuperGuide(string? username, bool isSuperGuide)
        {
            _userRepository.SetSuperGuide(username, isSuperGuide);
            _tourService.UpdateSuperGuide(username, isSuperGuide);
        }
    }
}
