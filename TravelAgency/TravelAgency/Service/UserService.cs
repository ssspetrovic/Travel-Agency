using TravelAgency.Interface;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    internal class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
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
        }
    }
}
