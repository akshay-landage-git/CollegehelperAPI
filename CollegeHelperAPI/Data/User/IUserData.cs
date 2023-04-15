using CollegeHelperAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeHelperAPI.Data
{
    internal interface IUserData
    {
        Task<UserModel> ValidateUser(SqlParameter[] sqlParams);
        Task<object> AddUser(SqlParameter[] sqlParams);
    }
}
