namespace MealPlannerProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using MealPlannerProject.Models;
    using MealPlannerProject.Queries;
    using MealPlannerProject.Repositories;

    public class MealService
    {
        private readonly MealRepository mealRepository;
        private readonly IngredientRepository ingredientRepository;

        public MealService()
        {
            this.mealRepository = new MealRepository();
            this.ingredientRepository = new IngredientRepository();
        }

        public async Task<bool> CreateMealAsync(Meal meal, string cookingLevel)
        {
            try
            {
                var cookingSkillId = this.GetCookingSkillId(cookingLevel);
                var mealTypeId = this.GetMealTypeId(meal.Category);

                var mealId = await this.mealRepository.CreateMealAsync(meal, cookingSkillId, mealTypeId);

                if (mealId > 0)
                {
                    Debug.WriteLine("Meal created successfully");
                    return true;
                }
                else
                {
                    Debug.WriteLine("Failed to create meal");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating meal: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }

        public async Task<List<Meal>> GetAllMealsAsync()
        {
            var meals = new List<Meal>();
            try
            {
                string query = @"
                SELECT m.m_name, m.recipe, mt.m_description as meal_type, m.calories, 
                       m.preparation_time, m.servings, m.protein, m.carbohydrates, 
                       m.fat, m.fiber, m.sugar, m.photo_link,
                       cs.cs_description as cooking_level
                FROM meals m
                JOIN meal_types mt ON m.mt_id = mt.mt_id
                JOIN cooking_skills cs ON m.cs_id = cs.cs_id";

                DataTable result = await Task.Run(() => DataLink.Instance.ExecuteReader(query));

                foreach (DataRow row in result.Rows)
                {
                    var meal = new Meal
                    {
                        Name = row["m_name"]?.ToString() ?? string.Empty,
                        Recipe = row["recipe"]?.ToString() ?? string.Empty,
                        Category = row["meal_type"]?.ToString() ?? string.Empty,
                        Calories = row["calories"] != DBNull.Value ? Convert.ToInt32(row["calories"]) : 0,
                        PreparationTime = row["preparation_time"] != DBNull.Value ? Convert.ToInt32(row["preparation_time"]) : 0,
                        Servings = row["servings"] != DBNull.Value ? Convert.ToInt32(row["servings"]) : 0,
                        Protein = row["protein"] != DBNull.Value ? Convert.ToInt32(row["protein"]) : 0,
                        Carbohydrates = row["carbohydrates"] != DBNull.Value ? Convert.ToInt32(row["carbohydrates"]) : 0,
                        Fat = row["fat"] != DBNull.Value ? Convert.ToInt32(row["fat"]) : 0,
                        Fiber = row["fiber"] != DBNull.Value ? Convert.ToInt32(row["fiber"]) : 0,
                        Sugar = row["sugar"] != DBNull.Value ? Convert.ToInt32(row["sugar"]) : 0,
                        PhotoLink = row["photo_link"]?.ToString() ?? string.Empty,
                        CookingLevel = row["cooking_level"]?.ToString() ?? string.Empty,
                    };
                    meals.Add(meal);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting meals: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            return meals;
        }

        public async Task<Ingredient?> GetIngredientByNameAsync(string name)
        {
            return await Task.Run(() => this.ingredientRepository.GetIngredientByNameAsync(name));
        }

        public async Task<int> CreateMealAsync(Meal meal)
        {
            try
            {
                var cookingSkillId = this.GetCookingSkillId(meal.CookingLevel);
                var mealTypeId = this.GetMealTypeId(meal.Category);

                return await this.mealRepository.CreateMealAsync(meal, cookingSkillId, mealTypeId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating meal: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        public async Task<bool> AddMealIngredientAsync(int mealId, int ingredientId, float quantity)
        {
            var result = await this.mealRepository.AddMealIngredientAsync(mealId, ingredientId, quantity);
            return result > 0;
        }

        private int GetMealTypeId(string category)
        {
            return category?.ToLower() switch
            {
                "breakfast" => 1,
                "lunch" => 2,
                "dinner" => 3,
                "snack" => 4,
                "dessert" => 5,
                "post-workout" => 6,
                "pre-workout" => 7,
                "vegan meal" => 8,
                "high-protein meal" => 9,
                "low-carb meal" => 10,
                _ => 1
            };
        }

        private int GetCookingSkillId(string category)
        {
            Debug.WriteLine($"GetCookingSkillId called with category: {category}");
            return category?.ToLower() switch
            {
                "beginner" => 1,
                "intermediate" => 2,
                "advanced" => 3,
                _ => 1
            };
        }
    }
}
