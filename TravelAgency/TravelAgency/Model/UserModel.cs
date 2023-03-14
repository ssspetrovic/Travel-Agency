
namespace TravelAgency.Model
{
    public enum Role
    {
        Owner = 1,
        Guide,
        Guest1,
        Guest2
    }
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public Role Role { get; set; }

        public UserModel()
        {

        }

        public UserModel(int id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }

        public UserModel(int id, string username, string password, string email, string name, string surname, Role role) : this(id, username, password)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Role = role;
        }


    }
}
