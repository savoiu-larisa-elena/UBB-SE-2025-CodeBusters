using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using MealPlannerProject.Models;
using MealPlannerProject.Queries;
using MealPlannerProject.Repositories;

namespace MealPlannerProject.Services
{
    public class MealService
    {
        private readonly MealRepository _mealRepository;
        private readonly IngredientRepository _ingredientRepository;

        public MealService()
        {
            _mealRepository = new MealRepository();
            _ingredientRepository = new IngredientRepository();
        }

        public async Task<bool> CreateMealAsync(Meal meal, string cookingLevel)
        {
            try
            {
                var cookingSkillId = GetCookingSkillId(cookingLevel);
                var mealTypeId = GetMealTypeId(meal.Category);

                var mealId = await _mealRepository.CreateMealAsync(meal, cookingSkillId, mealTypeId);

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

                DataTable result = DataLink.Instance.ExecuteReader(query);

                foreach (DataRow row in result.Rows)
                {
                    var meal = new Meal
                    {
                        Name = row["m_name"].ToString(),
                        Recipe = row["recipe"].ToString(),
                        Category = row["meal_type"].ToString(),
                        Calories = Convert.ToInt32(row["calories"]),
                        PreparationTime = Convert.ToInt32(row["preparation_time"]),
                        Servings = Convert.ToInt32(row["servings"]),
                        Protein = Convert.ToInt32(row["protein"]),
                        Carbohydrates = Convert.ToInt32(row["carbohydrates"]),
                        Fat = Convert.ToInt32(row["fat"]),
                        Fiber = Convert.ToInt32(row["fiber"]),
                        Sugar = Convert.ToInt32(row["sugar"]),
                        PhotoLink = row["photo_link"].ToString(),
                        CookingLevel = row["cooking_level"].ToString()
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

        public async Task<Ingredient> GetIngredientByNameAsync(string name)
        {
            return await Task.Run(() => _ingredientRepository.GetIngredientByNameAsync(name));
        }

        public async Task<int> CreateMealAsync(Meal meal)
        {
            try
            {
                var cookingSkillId = GetCookingSkillId(meal.CookingLevel);
                var mealTypeId = GetMealTypeId(meal.Category);

                return await _mealRepository.CreateMealAsync(meal, cookingSkillId, mealTypeId);
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
            var result = await _mealRepository.AddMealIngredientAsync(mealId, ingredientId, quantity);
            return result > 0;
        }
    }
}
