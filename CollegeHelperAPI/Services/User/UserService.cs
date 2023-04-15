using CollegeHelperAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using CollegeHelperAPI.Utility;
using CollegeHelperAPI.Data;
using CollegeHelperAPI.Data.User;
using System.Net;
using static CollegeHelperAPI.Models.Auth.LoginResponce;

namespace CollegeHelperAPI.Services
{
    public class UserService
    {
        public async Task<UserModel> ValidateUser(string email, string password)
        {
            UserData userData = new UserData();
            return await userData.ValidateUser(email, password);
        }

        public async Task<bool> AddUser(SignUpRequest signUpRequest)
        {
            signUpRequest.Password = HashedString.ConvertStringIntoHash(signUpRequest.Password);

            UserData userData = new UserData();
            return await userData.AddUser(signUpRequest);
        }
    }
}