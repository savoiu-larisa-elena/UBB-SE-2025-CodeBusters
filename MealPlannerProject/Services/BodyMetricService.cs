using MealPlannerProject.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Services
{
    class BodyMetricService
    {
        public void addBodyMetrics(string firstName, string lastName, string weight, string height, string target_goal)
        {
            //this uses the sql update function
            float user_weight = float.Parse(weight);
            float user_height = float.Parse(height);
            float user_target_goal = float.Parse(target_goal);
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_name", $"{lastName} {firstName}")
            };

            int u_id = DataLink.Instance.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);

            parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", u_id),
                new SqlParameter("@u_weight", user_weight),
                new SqlParameter("@u_height", user_height),
                new SqlParameter("@u_goal", user_target_goal)
            };

            DataLink.Instance.ExecuteNonQuery("UpdateUserBodyMetrics", parameters);
        }
    }
}
