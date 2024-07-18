using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Business.Model;
using Test.Entities;

namespace Test.Business.UserService
{
    public interface IUserService
    {
        Task<User> GetUserByUsername(string username);
        Task AddUser(UserRequestModel user);
    }
}
