using System.Collections.Generic;
using System.Net;

namespace TravelAgency.Model
{
    internal interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        Role GetRole(string username);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id);
        UserModel GetById(int id);
        UserModel GetByUsername(string? username);
        IEnumerable<UserModel> GetByAll();
    }
}
