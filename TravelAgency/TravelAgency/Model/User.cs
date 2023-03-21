
namespace TravelAgency.Model
{
    public enum Role
    {
        Owner = 1,
        Guide,
        Guest1,
        Tourist
    }
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Role Role { get; set; }

        public User()
        {

        }

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public User(int id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }

        public User(string username, string password, string name, string surname, string email, Role role) : this(username, password)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Role = role;
        }

        public User(int id, string username, string password, string name, string surname, string email, Role role) : this(id, username, password)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Role = role;
        }




    }
}
