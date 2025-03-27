using System;
using Microsoft.Data.SqlClient;

public class DatabaseHelper
{
    public static SqlConnection GetConnection()
    {
        return new SqlConnection(DatabaseConfig.ConnectionString);
    }

    public static bool TestConnection()
    {
        try
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database connection failed: {ex.Message}");
            return false;
        }
    }
}
