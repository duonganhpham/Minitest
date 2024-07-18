
using Test.Business.Model;
using Test.Entities;

namespace Test.Business.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
        Task AddUser(UserRequestModel user);
    }
}
