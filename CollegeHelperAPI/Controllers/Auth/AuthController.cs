
using CollegeHelperAPI.Enums;
using CollegeHelperAPI.Models;
using CollegeHelperAPI.Services;
using CollegeHelperAPI.Utility;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using static CollegeHelperAPI.Models.Auth.LoginResponce;

namespace CollegeHelperAPI.Controllers
{
    public class AuthController : ApiController
    {

        [ActionName("login")]
        [HttpPost]
        public async Task<HttpResponseMessage> Login([FromBody] LoginRequest credentails)
        {
            AuthService authService = new AuthService();
            credentails.Password = HashedString.ConvertStringIntoHash(credentails.Password);
            LoginResponse response = await authService.Authenticate(credentails);
            
            if (response.Status == LoginStatus.Success)
                return Request.CreateResponse(HttpStatusCode.OK, response);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [ActionName("sign-up")]
        [HttpPost]
        public async Task<HttpResponseMessage> signUp([FromBody] SignUpRequest credentails)
        {
            UserService userService = new UserService();
            bool response = await userService.AddUser(credentails);

            if (response)
                return Request.CreateResponse(HttpStatusCode.OK, response);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [ActionName("refreshAccessToken")]
        [HttpPost]
        public async Task<HttpResponseMessage> RefreshAccessToken([FromBody] LoggedInUser loggedInUser)
        {
            AuthService authService = new AuthService();
            return Request.CreateResponse(HttpStatusCode.OK, authService.RefreshToken(loggedInUser));
        }
    }
}
