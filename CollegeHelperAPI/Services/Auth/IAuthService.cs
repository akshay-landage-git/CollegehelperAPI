using CollegeHelperAPI.Models.Auth;
using CollegeHelperAPI.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CollegeHelperAPI.Models.Auth.LoginResponce;

namespace CollegeHelperAPI.Services
{
    internal interface IAuthService
    {
        //Task<LoginResponse> Authenticate(LoginRequest request);
        LoginResponse RefreshToken(LoggedInUser loggedInUser);
    }
}
