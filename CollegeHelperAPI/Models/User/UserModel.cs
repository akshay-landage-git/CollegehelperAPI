using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeHelperAPI.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public bool IsAdmin { get; set; }
        public bool Active { get; set; }
        public int CreatedByID  { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int ModifiedByID { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }

}