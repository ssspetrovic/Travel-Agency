using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal interface ICurrentUserRepository
    {
        void Add(string username);
        void Remove();
        CurrentUserModel Get();
    }
}
