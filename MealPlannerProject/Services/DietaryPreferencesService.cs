namespace MealPlannerProject.Services
{
    using System;
    using System.Data.SqlClient;
    using MealPlannerProject.Interfaces;
    using MealPlannerProject.Interfaces.Services;

    public class DietaryPreferencesService : IDietaryPreferencesService
    {
        private readonly IDataLink dataLink;

        public DietaryPreferencesService(IDataLink dataLink)
        {
            this.dataLink = dataLink;
        }
        [Obsolete]
        public void AddAllergyAndDietaryPreference(string firstName, string lastName, string dietaryPreference, string allergy)
        {
            // this uses the sql update function
            var parameters = new SqlParameter[]
            {
                new ("@u_name", $"{lastName} {firstName}"),
            };
            int u_id = this.dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);
            parameters =
            [
                new SqlParameter("@dp_description", dietaryPreference),
            ];
            int dp_id = this.dataLink.ExecuteScalar<int>("SELECT dbo.GetDietaryPreferencesByDescription(@dp_description)", parameters, false);
            parameters =
            [
                new SqlParameter("@a_description", allergy),
            ];
            int a_id = this.dataLink.ExecuteScalar<int>("SELECT dbo.GetAllergiesByDescription(@a_description)", parameters, false);
            parameters =
            [
                new SqlParameter("@u_id", u_id),
                new SqlParameter("@dp_id", dp_id),
            ];
            this.dataLink.ExecuteNonQuery("UpdateUserDietaryPreference", parameters);
            parameters =
            [
                new SqlParameter("@u_id", u_id),
                new SqlParameter("@a_id", a_id),
            ];
            this.dataLink.ExecuteNonQuery("UpdateUserAllergies", parameters);
        }
    }
}
