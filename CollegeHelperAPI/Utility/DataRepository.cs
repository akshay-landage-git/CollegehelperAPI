using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using CollegeHelperAPI.Enums;

namespace CollegeHelperAPI.Utility
{
    public class DataRepository
    {
        public List<SqlParameter> parameters = new List<SqlParameter>();

        public void AddParameter(string parameterName, SqlDbType dbType, object value)
        {
            SqlParameter param = new SqlParameter(parameterName, dbType);
            param.Value = value;
            parameters.Add(param);
        }

        public void AddParameter(string parameterName, SqlDbType dbType, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, dbType);
            param.Direction = direction;
            parameters.Add(param);
        }

        public DataSet FillData(string cmdText)
        {
            DbConnectionAppSetting dbConnectionAppSetting = new DbConnectionAppSetting();
            // Define the connection string
            string connectionString = dbConnectionAppSetting.ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(cmdText, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters.ToArray());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public int ExecuteQuery(string cmdText)
        {
            DbConnectionAppSetting dbConnectionAppSetting = new DbConnectionAppSetting();
            // Define the connection string
            string connectionString = dbConnectionAppSetting.ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(cmdText, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters.ToArray());
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public int ExecuteNonQuery(string procedureName)
        {
            DbConnectionAppSetting dbConnectionAppSetting = new DbConnectionAppSetting();
            // Define the connection string
            string connectionString = dbConnectionAppSetting.ConnectionString;

            int rowsAffected;
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters.ToArray());
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        public object GetParameterValue(string parameterName)
        {
            return parameters.FirstOrDefault(p => p.ParameterName == parameterName)?.Value;
        }
    }
}