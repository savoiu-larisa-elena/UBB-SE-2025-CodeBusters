// using Microsoft.Data.SqlClient;
using MealPlannerProject.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Services
{
    class CalorieService
    {

        public float GetGoal(int userId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            float goal = DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_calorie_goal(@UserId)", parameters, false);
            return goal;
        }

        public float GetFood(int userId)
        {
            var parameters = new SqlParameter[]
            {
        new SqlParameter("@UserId", userId)
            };

            float foodCal = DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_calorie_food(@UserId)", parameters, false);
            return foodCal;
        }

        public float GetExercise(int userId)
        {
            var parameters = new SqlParameter[]
            {
        new SqlParameter("@UserId", userId)
            };

            float exerciseCal = DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_calorie_exercise(@UserId)", parameters, false);
            return exerciseCal;
        }

    }
}
