namespace MealPlannerProject.Services
{
    using System.Data.SqlClient;
    using System.Diagnostics;
    using MealPlannerProject.Queries;

    internal class ActivityPageService
    {
        [System.Obsolete]
        public void AddActivity(string firstName, string lastName, string a_description)
        {
            Debug.WriteLine($"Adding activity {a_description} for user {firstName} {lastName}");
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
                new SqlParameter("@a_description", a_description),
            };
            int a_id = DataLink.Instance.ExecuteScalar<int>("SELECT dbo.GetActivityByDescription(@a_description)", parameters, false);

            parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", u_id),
                new SqlParameter("@a_id", a_id),
            };

            DataLink.Instance.ExecuteNonQuery("UpdateUserActivity", parameters);
        }
    }
}
