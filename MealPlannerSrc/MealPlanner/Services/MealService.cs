using Microsoft.Data.SqlClient;
using MealPlanner.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MealPlanner.Services
{
    public class MealService
    {
        private readonly string _connectionString;

        public MealService()
        {
            _connectionString = DatabaseConfig.ConnectionString;
        }

        public async Task<bool> CreateMealAsync(Meal meal, string cookingLevel)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"
                        INSERT INTO meals (
                            m_name, recipe, cs_id, dp_id, mt_id, 
                            preparation_time, servings, protein, calories, 
                            carbohydrates, fat, fiber, sugar
                        ) VALUES (
                            @Name, @Recipe, @CookingSkillId, @DietaryPreferenceId, @MealTypeId,
                            @PreparationTime, @Servings, @Protein, @Calories,
                            @Carbohydrates, @Fat, @Fiber, @Sugar
                        )";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", meal.Name);
                        command.Parameters.AddWithValue("@Recipe", meal.Recipe);
                        command.Parameters.AddWithValue("@CookingSkillId", GetCookingSkillId(cookingLevel));
                        command.Parameters.AddWithValue("@DietaryPreferenceId", 1); // Default to None
                        command.Parameters.AddWithValue("@MealTypeId", GetMealTypeId(meal.Category));
                        command.Parameters.AddWithValue("@PreparationTime", 30.0); // Default value
                        command.Parameters.AddWithValue("@Servings", 2.0); // Default value
                        command.Parameters.AddWithValue("@Protein", 30.0); // Default value
                        command.Parameters.AddWithValue("@Calories", meal.Calories);
                        command.Parameters.AddWithValue("@Carbohydrates", 65.0); // Default value
                        command.Parameters.AddWithValue("@Fat", 12.0); // Default value
                        command.Parameters.AddWithValue("@Fiber", 5.0); // Default value
                        command.Parameters.AddWithValue("@Sugar", 5.0); // Default value

                        await command.ExecuteNonQueryAsync();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Debug.WriteLine($"Error creating meal: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Meal>> GetAllMealsAsync()
        {
            var meals = new List<Meal>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Debug.WriteLine("Database connection opened successfully");

                    string query = @"
                        SELECT m_name, recipe, mt_id, calories, preparation_time, servings,
                               protein, carbohydrates, fat, fiber, sugar, photo_link
                        FROM meals";

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        Debug.WriteLine("Starting to read meals from database");
                        int count = 0;
                        while (await reader.ReadAsync())
                        {
                            count++;
                            var meal = new Meal
                            {
                                Name = reader.GetString(0),
                                Recipe = reader.GetString(1),
                                Category = GetMealTypeName(reader.GetInt32(2)),
                                Calories = reader.GetInt32(3),
                                PreparationTime = reader.GetInt32(4),
                                Servings = reader.GetInt32(5),
                                Protein = reader.GetInt32(6),
                                Carbohydrates = reader.GetInt32(7),
                                Fat = reader.GetInt32(8),
                                Fiber = reader.GetInt32(9),
                                Sugar = reader.GetInt32(10),
                                PhotoLink = reader.IsDBNull(11) ? "Assets/meals/default.png" : reader.GetString(11)
                            };
                            meals.Add(meal);
                            Debug.WriteLine($"Loaded meal: {meal.Name} with {meal.Calories} calories");
                        }
                        Debug.WriteLine($"Total meals loaded: {count}");
                    }
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
                _ => 1 // Default to Breakfast
            };
        }

        private string GetMealTypeName(int mealTypeId)
        {
            return mealTypeId switch
            {
                1 => "Breakfast",
                2 => "Lunch",
                3 => "Dinner",
                4 => "Snack",
                5 => "Dessert",
                6 => "Post-Workout",
                7 => "Pre-Workout",
                8 => "Vegan Meal",
                9 => "High-Protein Meal",
                10 => "Low-Carb Meal",
                _ => "Breakfast" // Default
            };
        }

        private int GetCookingSkillId(string category)
        {
            return category?.ToLower() switch
            {
                "beginner" => 1,
                "intermediate" => 2,
                "advanced" => 3,
                _ => 1 // Default to Beginner
            };
        }
    }
}
