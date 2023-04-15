using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using CollegeHelperAPI.Enums;
namespace CollegeHelperAPI.Utility
{
    public static class DBConnection
    {
        public static SqlConnection DbConnection()
        {
            DbConnectionAppSetting dbConnectionAppSetting = new DbConnectionAppSetting();
            // Define the connection string
            string connectionString = dbConnectionAppSetting.ConnectionString;

            // Create a SqlConnection object
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                // Open the connection
                connection.Open();
                Console.WriteLine("Database connection established.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection
                connection.Close();
                Console.WriteLine("Database connection closed.");
            }
            return connection;

        }
        //public static SqlConnection GetDataSetAsync(string StoredProcedure, SqlParameter sqlParameter)
        //{
            
        //    SqlCommand command = new SqlCommand(StoredProcedure, connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(sqlParameter);
        //    command.ExecuteNonQuery();

        //    return connection;

        //}

        public static List<T> ConvertToList<T>(this DataTable dt)
        {
            List<string> columnNames = dt.Columns.Cast<DataColumn>()
            .Select(c => c.ColumnName)
            .ToList();

            PropertyInfo[] properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                T objT = Activator.CreateInstance<T>();
                foreach (PropertyInfo pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        pro.SetValue(objT, row.IsNull(pro.Name) ? null : row[pro.Name], null);
                    }
                }
                return objT;

            }).ToList();
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }

}