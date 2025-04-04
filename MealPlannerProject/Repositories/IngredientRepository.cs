using MealPlannerProject.Models;
using MealPlannerProject.Queries;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MealPlannerProject.Repositories
{
    public class IngredientRepository
    {
        public async Task<Ingredient?> GetIngredientByNameAsync(string name)
        {
            string query = @"SELECT i_id, i_name, calories, protein, carbohydrates, fat, fiber, sugar 
                             FROM ingredients 
                             WHERE i_name = @name;";

            var parameters = new SqlParameter[]
            {
                new("@name", name)
            };

            DataTable result = await Task.Run(() => DataLink.Instance.ExecuteSqlQuery(query, parameters));

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Ingredient
                {
                    Id = Convert.ToInt32(row["i_id"]),
                    Name = row["i_name"].ToString(),
                    Calories = Convert.ToSingle(row["calories"]),
                    Protein = Convert.ToSingle(row["protein"]),
                    Carbs = Convert.ToSingle(row["carbohydrates"]),
                    Fats = Convert.ToSingle(row["fat"]),
                    Fiber = Convert.ToSingle(row["fiber"]),
                    Sugar = Convert.ToSingle(row["sugar"])
                };
            }

            return null;
        }
    }
}
