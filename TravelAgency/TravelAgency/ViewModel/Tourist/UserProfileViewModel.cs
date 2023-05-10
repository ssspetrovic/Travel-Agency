using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class UserProfileViewModel
    {
        public string? AccountType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }

        public UserProfileViewModel()
        {
            var userService = new UserService();
            var user = userService.GetByUsername(CurrentUser.Username);
            AccountType = user.Role.ToString();
            FirstName = user.Name;
            LastName = user.Surname;
            Email = user.Email;
            Username = user.UserName;
        }
    }
}
