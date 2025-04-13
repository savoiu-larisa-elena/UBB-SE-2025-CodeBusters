// IDataLink Interface
using System.Data;

namespace MealPlannerProject.Interfaces
{
    public interface IDataLink
    {
        DataTable ExecuteSqlQuery(string query, System.Data.SqlClient.SqlParameter[] parameters);
    }
}
