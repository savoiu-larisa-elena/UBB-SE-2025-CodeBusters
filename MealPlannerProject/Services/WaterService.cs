namespace MealPlannerProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.ConstrainedExecution;
    using System.Text;
    using System.Threading.Tasks;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.Queries;

    public class WaterService : IWaterService
    {
        public DataLink dataLink;

        public WaterService()
        {
            this.dataLink = DataLink.Instance;
        }

        [Obsolete]
        public float GetWaterIntake(int userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return this.dataLink.ExecuteScalar<float>("SELECT dbo.get_water_intake(@UserId)", parameters, false);
        }

        [Obsolete]
        public void UpdateWaterIntake(int userId, float newIntake)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@NewIntake", newIntake),
            };
            this.dataLink.ExecuteQuery("exec update_water_intake @UserId, @NewIntake", parameters, false);
        }

        [Obsolete]
        public void RemoveWater300(int userId)
        {
            const float WATER_DIFFERENCE_300 = 300f;
            float currentIntake = this.GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - WATER_DIFFERENCE_300); // Ensure we don't go below 0
            this.UpdateWaterIntake(userId, newIntake);
        }

        [Obsolete]
        public void RemoveWater400(int userId)
        {
            const float WATER_DIFFERENCE_400 = 400f;
            float currentIntake = this.GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - WATER_DIFFERENCE_400); // Ensure we don't go below 0
            this.UpdateWaterIntake(userId, newIntake);
        }

        [Obsolete]
        public void RemoveWater500(int userId)
        {
            const float WATER_DIFFERENCE_500 = 500f;
            float currentIntake = this.GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - WATER_DIFFERENCE_500); // Ensure we don't go below 0
            this.UpdateWaterIntake(userId, newIntake);
        }

        [Obsolete]
        public void RemoveWater750(int userId)
        {
            const float WATER_DIFFERENCE_750 = 750f;
            float currentIntake = this.GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - WATER_DIFFERENCE_750); // Ensure we don't go below 0
            this.UpdateWaterIntake(userId, newIntake);
        }
    }
}
