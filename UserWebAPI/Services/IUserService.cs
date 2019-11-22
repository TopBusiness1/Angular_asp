using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWebAPI.Entities;

namespace UserWebAPI.Services
{
    public interface IUserService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
    }
}
