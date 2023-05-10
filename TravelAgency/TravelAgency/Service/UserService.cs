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
    }
}
