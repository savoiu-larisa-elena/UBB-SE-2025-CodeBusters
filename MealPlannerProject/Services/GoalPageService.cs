using System.Data.SqlClient;
using System.Diagnostics;
using MealPlannerProject.Exceptions;
using MealPlannerProject.Queries;
using MealPlannerProject.Interfaces;

    internal class GoalPageService
    {
        [System.Obsolete]
        public void AddGoals(string firstName, string lastName, string g_description)
        {
            Debug.WriteLine($"Adding goal {g_description} for user {firstName} {lastName}");
            string u_name = lastName + " " + firstName;
            var parameters = new SqlParameter[]
            {
               new SqlParameter("@u_name", u_name),
            };
            Debug.WriteLine($"User name: {u_name}");

            int u_id = DataLink.Instance.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);

            Debug.WriteLine($"User ID: {u_id}");

            parameters = new SqlParameter[]
            {
                new SqlParameter("@g_description", g_description),
            };

            int g_id = DataLink.Instance.ExecuteScalar<int>("SELECT dbo.GetGoalByDescription(@g_description)", parameters, false);


            parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", u_id),
                new SqlParameter("@g_id", g_id),
            };

            DataLink.Instance.ExecuteNonQuery("UpdateUserGoals", parameters);
        }
    }
}
