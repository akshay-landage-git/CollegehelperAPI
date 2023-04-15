using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CollegeHelperAPI.Utility
{
    public class HashedString
    {
        public static string ConvertStringIntoHash(string str)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

                // Create a new StringBuilder to collect the bytes
                StringBuilder sb = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string
                return sb.ToString();
            }
        }
    }
}