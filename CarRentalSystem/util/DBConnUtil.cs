using System.Data.SqlClient;
using System.Collections.Generic;

namespace CarRentalSystem.util
{
    public class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            string propertyFilePath = "db.properties"; // same folder as .exe or give full path
            Dictionary<string, string> props = DBPropertyUtil.GetProperties(propertyFilePath);

            string connectionString = $"Data Source={props["host"]};Initial Catalog={props["database"]};Integrated Security=True";
            return new SqlConnection(connectionString);
        }
    }
}
