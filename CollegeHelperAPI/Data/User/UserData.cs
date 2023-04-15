using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CollegeHelperAPI.Models;
using CollegeHelperAPI.Utility;
using System.Web.Http.Results;

namespace CollegeHelperAPI.Data.User
{
    public class UserData
    {
        public async Task<UserModel> ValidateUser(string email, string password)
        {
            SqlConnection con = DBConnection.DbConnection();
            List<UserModel> users = new List<UserModel>();
            using (con)
            {
                using (SqlCommand command = new SqlCommand("UserValidate", con))
                {
                    con.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Users");
                    DataTable UsersTable = dataSet.Tables["Users"];
                    List<UserModel> userModel = DBConnection.ConvertToList<UserModel>(UsersTable);
                    con.Close();
                    if (userModel.Count > 0)
                    {
                        return userModel[0];
                    }
                    else { return null; }
                }
            }
        }

        //public async Task<bool> AddUser(SignUpRequest signUpRequest)
        //{
        //    using (SqlConnection con = DBConnection.DbConnection())
        //    {
        //        using (SqlCommand command = new SqlCommand("UserAdd", con))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            command.Parameters.AddWithValue("@FirstName", signUpRequest.FirstName);
        //            command.Parameters.AddWithValue("@LastName", signUpRequest.LastName);
        //            command.Parameters.AddWithValue("@Email", signUpRequest.Email);
        //            command.Parameters.AddWithValue("@Password", signUpRequest.Password);
        //            command.Parameters.AddWithValue("@MobileNumber", signUpRequest.MobileNumber);

        //            con.Open();
        //            await command.ExecuteNonQueryAsync();
        //            con.Close();
        //            int userID = Convert.ToInt32(command.Parameters["@UserID"].Value);

        //            return userID > 0;
        //        }
        //    }
        //}

        //public async Task<bool> AddUser(SignUpRequest signUpRequest)
        //{
        //    using (SqlConnection con = DBConnection.DbConnection())
        //    {
        //        using (SqlCommand command = new SqlCommand("UserAdd", con))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@UserID", 0);
        //            command.Parameters.AddWithValue("@Password", signUpRequest.Password);
        //            command.Parameters.AddWithValue("@FirstName", signUpRequest.FirstName);
        //            command.Parameters.AddWithValue("@LastName", signUpRequest.LastName);
        //            command.Parameters.AddWithValue("@MobileNumber", signUpRequest.MobileNumber);
        //            command.Parameters.AddWithValue("@Email", signUpRequest.Email);

        //            con.Open();
        //            var result = await command.ExecuteScalarAsync();
        //            con.Close();

        //            int rowsAffected = Convert.ToInt32(result);
        //            if (rowsAffected > 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //}

        //public async Task<bool> AddUser(SignUpRequest signUpRequest)
        //{
        //    using (SqlConnection con = DBConnection.DbConnection())
        //    {
        //        using (SqlCommand command = new SqlCommand("UserAdd", con))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@UserID", 0);
        //            command.Parameters.AddWithValue("@Password", signUpRequest.Password);
        //            command.Parameters.AddWithValue("@FirstName", signUpRequest.FirstName);
        //            command.Parameters.AddWithValue("@LastName", signUpRequest.LastName);
        //            command.Parameters.AddWithValue("@MobileNumber", signUpRequest.MobileNumber);
        //            command.Parameters.AddWithValue("@Email", signUpRequest.Email);

        //            con.Open();
        //            var result = await command.ExecuteScalarAsync();
        //            con.Close();

        //            int rowsAffected = Convert.ToInt32(result);
        //            if (rowsAffected > 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //}

        //public async Task<bool> AddUser(SignUpRequest signUpRequest)
        //{
        //    using (SqlConnection con = DBConnection.DbConnection())
        //    {
        //        using (SqlCommand command = new SqlCommand("UserAdd", con))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            command.Parameters.AddWithValue("@Password", signUpRequest.Password);
        //            command.Parameters.AddWithValue("@FirstName", signUpRequest.FirstName);
        //            command.Parameters.AddWithValue("@LastName", signUpRequest.LastName);
        //            command.Parameters.AddWithValue("@MobileNumber", signUpRequest.MobileNumber);
        //            command.Parameters.AddWithValue("@Email", signUpRequest.Email);

        //            con.Open();
        //            await command.ExecuteNonQueryAsync();
        //            con.Close();

        //            int rowsAffected = Convert.ToInt32(command.Parameters["@UserID"].Value);
        //            if (rowsAffected > 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //}

        public async Task<bool> AddUser(SignUpRequest signUpRequest)
        {
            DataRepository db = new DataRepository();
            db.AddParameter("@Password", SqlDbType.NVarChar, signUpRequest.Password);
            db.AddParameter("@FirstName", SqlDbType.VarChar, signUpRequest.FirstName);
            db.AddParameter("@LastName", SqlDbType.VarChar, signUpRequest.LastName);
            db.AddParameter("@MobileNumber", SqlDbType.VarChar, signUpRequest.MobileNumber);
            db.AddParameter("@Email", SqlDbType.VarChar, signUpRequest.Email);
            db.AddParameter("@UserID", SqlDbType.Int, ParameterDirection.Output);
            int rowsAffected = db.ExecuteNonQuery("UserAdd");
            int userID = Convert.ToInt32(db.GetParameterValue("@UserID"));
            return userID>0;
        }

    }
}