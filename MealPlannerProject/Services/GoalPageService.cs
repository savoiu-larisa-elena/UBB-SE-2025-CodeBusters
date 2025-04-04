using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MealPlannerProject.Queries;
using System.Diagnostics;

namespace MealPlannerProject.Services
{
    class GoalPageService
    {
        public void addGoals(string FirstName, string LastName, string g_description)
        {
            Debug.WriteLine($"Adding goal {g_description} for user {FirstName} {LastName}");
            string u_name = LastName + " " + FirstName;
            var parameters = new SqlParameter[]
            {
               new SqlParameter("@u_name", u_name)
            };
            Debug.WriteLine($"User name: {u_name}");

            int u_id = DataLink.Instance.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);

            Debug.WriteLine($"User ID: {u_id}");

            parameters = new SqlParameter[]
            {
                new SqlParameter("@g_description", g_description)
            };

            int g_id = DataLink.Instance.ExecuteScalar<int>("SELECT dbo.GetGoalByDescription(@g_description)", parameters, false);


            parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", u_id),
                new SqlParameter("@g_id", g_id)
            };

            DataLink.Instance.ExecuteNonQuery("UpdateUserGoals", parameters);
        }
    }
}
