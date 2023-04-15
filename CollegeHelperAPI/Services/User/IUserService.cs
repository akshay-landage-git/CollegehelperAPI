using CollegeHelperAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeHelperAPI.Services
{
    internal interface IUserService
    {
        Task<UserModel> ValidateUser(string userName, string password);
        Task<object> AddUser(string userName, string password);
    }
}
