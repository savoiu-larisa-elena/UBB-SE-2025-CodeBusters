using MealPlannerProject.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlannerProject.Interfaces.Services;
namespace MealPlannerProject.Services
{
    /// <summary>
    /// Service for managing body metrics of users.
    /// </summary>
    public class BodyMetricService : IBodyMetricService
    {
        /// <summary>
        /// Updates the body metrics of a user in the database.
        /// </summary>
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="weight">The weight of the user.</param>
        /// <param name="height">The height of the user.</param>
        /// <param name="targetGoal">The target goal of the user.</param>
        public void UpdateUserBodyMetrics(string firstName, string lastName, string weight, string height, string targetGoal)
        {
            float userWeight = float.Parse(weight);
            float userHeight = float.Parse(height);
            float? userTargetGoal;
            if (targetGoal == "")
                userTargetGoal = null;
            else
                userTargetGoal = float.Parse(targetGoal);

            var parameters = new SqlParameter[]
            {
                    new SqlParameter("@u_name", $"{lastName} {firstName}")
            };

            int userId = DataLink.Instance.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);

            parameters = new SqlParameter[]
            {
                    new SqlParameter("@u_id", userId),
                    new SqlParameter("@u_weight", userWeight),
                    new SqlParameter("@u_height", userHeight),
                    new SqlParameter("@u_goal", userTargetGoal == null? DBNull.Value: userTargetGoal),
            };

            DataLink.Instance.ExecuteNonQuery("UpdateUserBodyMetrics", parameters);
        }
    }
}
