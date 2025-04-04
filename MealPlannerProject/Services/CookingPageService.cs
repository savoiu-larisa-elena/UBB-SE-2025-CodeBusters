using MealPlannerProject.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace MealPlannerProject.Services
{
    class CookingPageService
    {
        public void addCookingSkill(string FirstName, string LastName, string c_description)
        {
            Debug.WriteLine($"Adding cooking skill {c_description} for user {FirstName} {LastName}");
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
                new SqlParameter("@cs_description", c_description)
            };
            int c_id = DataLink.Instance.ExecuteScalar<int>("SELECT dbo.GetCookingSkillByDescription(@cs_description)", parameters, false);
            parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", u_id),
                new SqlParameter("@cs_id", c_id)
            };
            DataLink.Instance.ExecuteNonQuery("UpdateUserCookingSkill", parameters);
        }
    }
}
