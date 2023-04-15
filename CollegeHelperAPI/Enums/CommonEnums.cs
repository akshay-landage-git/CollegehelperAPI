using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeHelperAPI.Enums
{
    public class JWT
    {
        public string SecurityKey = "CollegeHelper_APL2023";
        public string Issuer = "https://localhost:44315";
        public string Audience = "http://localhost:4200";
        public int JwtExpireMinutes = 1440;
    }

    public enum LoginType
    {
        SystemRegistered = 1,
        GoogleUser = 2,
        MicrosoftUser = 3
    }
    
    public class DbConnectionAppSetting
    {
        public string ConnectionString = "Data Source=AKSHAY;Initial Catalog=CollegeHelper;Persist Security Info=True;User ID=CollegeHelper;Password=CollegeHelper@Akshay;";
    }
}