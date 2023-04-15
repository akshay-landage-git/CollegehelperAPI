using CollegeHelperAPI.Enums;
using CollegeHelperAPI.Models.Auth;
using CollegeHelperAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using static CollegeHelperAPI.Models.Auth.LoginResponce;
using CollegeHelperAPI.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CollegeHelperAPI.Utility;

namespace CollegeHelperAPI.Services
{
    public class AuthService
    {
        public AuthService() { }

        public async Task<LoginResponse> Authenticate(LoginRequest request)
        {
            UserService userService = new UserService();
            UserModel user = new UserModel();
            if (request.UserLoginType == LoginType.GoogleUser)
            {
                //if (!await IsValidGoogleToken(request.GoogleToken, request.Email))
                //{
                //    return new LoginResponse() { Status = LoginStatus.Invalid, Message = "Invalid User Account" };
                //}
                //else
                //{
                //    user = await userService.ValidateSingleSignOnUser(request.Email, LoginType.GoogleUser);
                //}
            }
            else if (request.UserLoginType == LoginType.MicrosoftUser)
            {
                //if (!await IsValidMicrosoftToken(request.MicrosoftToken))
                //{
                //    return new LoginResponse() { Status = LoginStatus.Invalid, Message = "Invalid User Account" };
                //}
                //else
                //{
                //    user = await userService.ValidateSingleSignOnUser(request.Email, LoginType.MicrosoftUser);
                //}
            }
            else
            {
                //if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                //{
                //    return new LoginResponse() { Status = LoginStatus.Invalid, Message = "Invalid Credentials!" };
                //}

                //user = await userService.ValidateUser(request.Email, request.Password);
            }

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new LoginResponse() { Status = LoginStatus.Invalid, Message = "Invalid Credentials!" };
            }

            user = await userService.ValidateUser(request.Email, request.Password);

            if (user != null && user?.UserID > 0)
            {
                return CreateSuccessLoginResult(user);
            }
            else
            {
                return new LoginResponse() { Status = LoginStatus.Invalid, Message = "Wrong username or password" };
            }
        }

        public LoginResponse RefreshToken(LoggedInUser loggedInUser)
        {
            UserModel user = new UserModel()
            {
                Email = loggedInUser.Email,
                FirstName = loggedInUser.FirstName,
                UserID = loggedInUser.UserID,
                LastName = loggedInUser.LastName
            };

            return CreateSuccessLoginResult(user);
        }

        private LoginResponse CreateSuccessLoginResult(UserModel user)
        {
            //user = GetUserDetails(user.UserID);

            LoginResponse loginResult = new LoginResponse()
            {
                AccessToken = CreateAccessToken(user),
                user = user,
                Status = LoginStatus.Success,
                Message = "Success"
            };

            return loginResult;
        }

        private string CreateAccessToken(UserModel user)
        {

            JWT jwt = new JWT();

            // set the secret key used to sign the JWT
            var secretKey = jwt.SecurityKey;
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // create a list of claims for the user
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "admin")
            };

            // create the JWT token
            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwt.JwtExpireMinutes),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            // serialize the JWT token to a string
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

    }
}