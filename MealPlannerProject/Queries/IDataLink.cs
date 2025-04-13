using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MealPlannerProject.Queries
{
    public interface IDataLink
    {
        T? ExecuteScalar<T>(string query, SqlParameter[]? sqlParameters = null, bool isStoredProcedure = true);
        int ExecuteQuery(string query, SqlParameter[]? sqlParameters = null, bool isStoredProcedure = true);
        T? ExecuteBool<T>(string storedProcedure, SqlParameter[]? sqlParameters = null);
        DataTable ExecuteReader(string storedProcedure, SqlParameter[]? sqlParameters = null);
        int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters = null);
        Task<int> ExecuteNonQueryAsync(string storedProcedure, SqlParameter[] sqlParameters = null);
        DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters = null);
    }
}
