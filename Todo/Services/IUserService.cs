using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public interface IUserService
    {
        Task SaveUserAsync(User user);
        //implemented class (UserService):
        //json file create if not alredy created
        //if created AddUser.razor(we are trying to inject the service here) bata aako data list append json file update
        //read user --> already created json file ko eata tanne
        Task<List<User>> LoadUsersAsync();
    }
}
