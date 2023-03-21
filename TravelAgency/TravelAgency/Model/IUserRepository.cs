using System.Collections.Generic;
using System.Net;

namespace TravelAgency.Model
{
    internal interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        Role GetRole(string username);
        void Add(User user);
        void Edit(User user);
        void Remove(int id);
        User GetById(int id);
        User GetByUsername(string? username);
        IEnumerable<User> GetByAll();
    }
}
