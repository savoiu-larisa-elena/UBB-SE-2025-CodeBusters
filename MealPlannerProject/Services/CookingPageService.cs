namespace MealPlannerProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.Queries;
    using Windows.ApplicationModel.Appointments.AppointmentsProvider;

    internal class CookingPageService : ICookingPageService
    {
        [Obsolete]
        public void AddCookingSkill(string firstName, string lastName, string cookingDescription)
        {
            Debug.WriteLine($"Adding cooking skill {cookingDescription} for user {firstName} {lastName}");
            string u_name = lastName + " " + firstName;
            var parameters = new SqlParameter[]
            {
               new SqlParameter("@u_name", u_name),
            };
            Debug.WriteLine($"User name: {u_name}");
            int user_id = DataLink.Instance.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);
            Debug.WriteLine($"User ID: {user_id}");
            parameters = new SqlParameter[]
            {
                new SqlParameter("@cs_description", cookingDescription),
            };
            int cookingSkill_id = DataLink.Instance.ExecuteScalar<int>("SELECT dbo.GetCookingSkillByDescription(@cs_description)", parameters, false);
            parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", user_id),
                new SqlParameter("@cs_id", cookingSkill_id),
            };
            DataLink.Instance.ExecuteNonQuery("UpdateUserCookingSkill", parameters);
        }
    }
}
