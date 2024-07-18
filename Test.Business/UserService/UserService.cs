using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Business.Model;
using Test.Business.UserRepository;
using Test.Data;
using Test.Entities;

namespace Test.Business.UserService
{
    public class UserService : IUserService
    {
       private readonly IUserRepository _userRepository;
       public UserService(IUserRepository userRepository)
       {
            _userRepository = userRepository;
       }
       public async Task<User> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }

        public async Task AddUser(UserRequestModel user)
        {
            await _userRepository.AddUser(user);
        }
    }
}
