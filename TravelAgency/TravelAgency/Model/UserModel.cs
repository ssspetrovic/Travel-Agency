
namespace TravelAgency.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }


        public UserModel(int id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }

        public UserModel(int id, string username, string password, string email, string name, string surname) : this(id, username, password)
        {
            Email = email;
            Name = name;
            Surname = surname;
        }


    }
}
