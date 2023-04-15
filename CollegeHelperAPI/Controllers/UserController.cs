using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Cryptography;
using System.Data.SqlClient;
using CollegeHelperAPI.Utility;
using CollegeHelperAPI.Models;
using System.Data;

namespace CollegeHelperAPI.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UserAdd()
        {
            var userModel = new HttpResponseMessage();
            string accessToken = "";

            SqlConnection con = DBConnection.DbConnection();

            UserModel users = new UserModel();
            using (con)
            {
                using (SqlCommand command = new SqlCommand("UserGet", con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", users.UserID);
                    command.Parameters.AddWithValue("@Password", users.Password);
                    command.Parameters.AddWithValue("@FirstName", users.FirstName);
                    command.Parameters.AddWithValue("@LastName", users.LastName);
                    command.Parameters.AddWithValue("@MobileNumber", users.MobileNumber);
                    command.Parameters.AddWithValue("@Email", users.Email);
                    command.Parameters.AddWithValue("@RoleID", users.RoleID);
                    command.Parameters.AddWithValue("@IsAdmin", users.IsAdmin);
                    command.Parameters.AddWithValue("@Active", users.Active);
                    command.Parameters.AddWithValue("@CreatedByID", users.CreatedByID);

                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, accessToken);
        }
    }
}
