namespace MealPlannerProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MealPlannerProject.Queries;

    internal class WaterService
    {
        public float GetWaterIntake(int userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_water_intake(@UserId)", parameters, false);
        }

        public void UpdateWaterIntake(int userId, float newIntake)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@NewIntake", newIntake)
            };
            DataLink.Instance.ExecuteQuery("exec update_water_intake @UserId, @NewIntake", parameters, false);
        }

        public void RemoveWater300(int userId)
        {
            float currentIntake = GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - 300); // Ensure we don't go below 0
            UpdateWaterIntake(userId, newIntake);
        }

        public void RemoveWater400(int userId)
        {
            float currentIntake = GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - 400); // Ensure we don't go below 0
            UpdateWaterIntake(userId, newIntake);
        }

        public void RemoveWater500(int userId)
        {
            float currentIntake = GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - 500); // Ensure we don't go below 0
            UpdateWaterIntake(userId, newIntake);
        }

        public void RemoveWater750(int userId)
        {
            float currentIntake = GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - 750); // Ensure we don't go below 0
            UpdateWaterIntake(userId, newIntake);
        }
    }
}
