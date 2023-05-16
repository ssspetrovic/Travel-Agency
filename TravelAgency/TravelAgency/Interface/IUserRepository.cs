using System.Net;
using TravelAgency.Model;

namespace TravelAgency.Interface
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        Role GetRole(string username);
        User GetById(int id);
        User GetByUsername(string? username);
    }
}
