using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeHelperAPI.Models.Auth
{
    public class LoginResponce
    {
        public class LoginResponse
        {
            public LoginStatus Status { get; set; }
            public string Message { get; set; }
            public object AccessToken { get; set; }
            public UserModel user { get; set; }
        }

        public enum LoginStatus
        {
            Invalid = 0,
            Success = 1,
            Inactive = 2
        }
    }
}