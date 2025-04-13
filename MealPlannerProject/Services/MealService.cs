namespace MealPlannerProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.Models;
    using MealPlannerProject.Queries;
    using MealPlannerProject.Repositories;

namespace MealPlannerProject.Services
{
    public class MealService
    {
        private readonly MealRepository _mealRepository;
        private readonly IngredientRepository _ingredientRepository;
        private DataLink dataLink;

        private const int BreakfastTypeId = 1;
        private const int LunchTypeId = 2;
        private const int DinnerTypeId = 3;
        private const int SnackTypeId = 4;
        private const int DessertTypeId = 5;
        private const int PostWorkoutTypeId = 6;
        private const int PreWorkoutTypeId = 7;
        private const int VeganMealTypeId = 8;
        private const int HighProteinMealTypeId = 9;
        private const int LowCarbMealTypeId = 10;
        private const int DefaultMealTypeId = 1;

        private const int BeginnerSkillId = 1;
        private const int IntermediateSkillId = 2;
        private const int AdvancedSkillId = 3;
        private const int DefaultCookingSkillId = 1;

        private readonly MealRepository mealDatabaseRepository;
        private readonly IngredientRepository ingredientDatabaseRepository;

        public MealService()
        {
            this.mealDatabaseRepository = new MealRepository();
            this.ingredientDatabaseRepository = new IngredientRepository();
        }

        public async Task<bool> CreateMealWithCookingLevelAsync(Meal mealToCreate, string cookingLevelDescription)
        {
            try
            {
                int cookingSkillIdentifier = this.ResolveCookingSkillIdentifier(cookingLevelDescription);
                int mealTypeIdentifier = this.ResolveMealTypeIdentifier(mealToCreate.Category);

                int createdMealIdentifier = await this.mealDatabaseRepository.CreateMealAsync(
                    mealToCreate,
                    cookingSkillIdentifier,
                    mealTypeIdentifier);

                if (createdMealIdentifier > SuccessfulCreationIndicator)
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
            catch (Exception exception)
            {
                Debug.WriteLine($"Error creating meal: {exception.Message}");
                Debug.WriteLine($"Stack trace: {exception.StackTrace}");
                return false;
            }
        }

        public async Task<List<Meal>> GetAllMealsAsync()
        {
            List<Meal> retrievedMeals = new List<Meal>();
            try
            {
                string sqlQueryText = @"
                SELECT m.m_name, m.recipe, mt.m_description as meal_type, m.calories, 
                       m.preparation_time, m.servings, m.protein, m.carbohydrates, 
                       m.fat, m.fiber, m.sugar, m.photo_link,
                       cs.cs_description as cooking_level
                FROM meals m
                JOIN meal_types mt ON m.mt_id = mt.mt_id
                JOIN cooking_skills cs ON m.cs_id = cs.cs_id";

                DataTable queryResultTable = await Task.Run(() => DataLink.Instance.ExecuteReader(sqlQueryText));

                foreach (DataRow dataRow in queryResultTable.Rows)
                {
                    Meal mealFromDatabase = new Meal
                    {
                        Name = dataRow["m_name"]?.ToString() ?? string.Empty,
                        Recipe = dataRow["recipe"]?.ToString() ?? string.Empty,
                        Category = dataRow["meal_type"]?.ToString() ?? string.Empty,
                        Calories = dataRow["calories"] != DBNull.Value ? Convert.ToInt32(dataRow["calories"]) : 0,
                        PreparationTime = dataRow["preparation_time"] != DBNull.Value ? Convert.ToInt32(dataRow["preparation_time"]) : 0,
                        Servings = dataRow["servings"] != DBNull.Value ? Convert.ToInt32(dataRow["servings"]) : 0,
                        Protein = dataRow["protein"] != DBNull.Value ? Convert.ToInt32(dataRow["protein"]) : 0,
                        Carbohydrates = dataRow["carbohydrates"] != DBNull.Value ? Convert.ToInt32(dataRow["carbohydrates"]) : 0,
                        Fat = dataRow["fat"] != DBNull.Value ? Convert.ToInt32(dataRow["fat"]) : 0,
                        Fiber = dataRow["fiber"] != DBNull.Value ? Convert.ToInt32(dataRow["fiber"]) : 0,
                        Sugar = dataRow["sugar"] != DBNull.Value ? Convert.ToInt32(dataRow["sugar"]) : 0,
                        PhotoLink = dataRow["photo_link"]?.ToString() ?? string.Empty,
                        CookingLevel = dataRow["cooking_level"]?.ToString() ?? string.Empty,
                    };
                    retrievedMeals.Add(mealFromDatabase);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error getting meals: {exception.Message}");
                Debug.WriteLine($"Stack trace: {exception.StackTrace}");
            }

            return retrievedMeals;
        }

        public async Task<Ingredient?> RetrieveIngredientByNameAsync(string ingredientName)
        {
            return await Task.Run(() => this.ingredientDatabaseRepository.GetIngredientByNameAsync(ingredientName));
        }

        public async Task<int> CreateMealAsync(Meal mealToCreate)
        {
            try
            {
                int cookingSkillIdentifier = this.ResolveCookingSkillIdentifier(mealToCreate.CookingLevel);
                int mealTypeIdentifier = this.ResolveMealTypeIdentifier(mealToCreate.Category);

                return await this.mealDatabaseRepository.CreateMealAsync(
                    mealToCreate,
                    cookingSkillIdentifier,
                    mealTypeIdentifier);
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error creating meal: {exception.Message}");
                Debug.WriteLine($"Stack trace: {exception.StackTrace}");
                return FailedOperationCode;
            }
        }

        public async Task<bool> AddIngredientToMealAsync(int mealIdentifier, int ingredientIdentifier, float ingredientQuantity)
        {
            int operationResult = await this.mealDatabaseRepository.AddMealIngredientAsync(
                mealIdentifier,
                ingredientIdentifier,
                ingredientQuantity);

            return operationResult > SuccessfulCreationIndicator;
        }

        private int ResolveMealTypeIdentifier(string mealCategory)
        {
            return mealCategory?.ToLower() switch
            {
                "breakfast" => BreakfastTypeId,
                "lunch" => LunchTypeId,
                "dinner" => DinnerTypeId,
                "snack" => SnackTypeId,
                "dessert" => DessertTypeId,
                "post-workout" => PostWorkoutTypeId,
                "pre-workout" => PreWorkoutTypeId,
                "vegan meal" => VeganMealTypeId,
                "high-protein meal" => HighProteinMealTypeId,
                "low-carb meal" => LowCarbMealTypeId,
                _ => DefaultMealTypeId
            };
        }

        private int ResolveCookingSkillIdentifier(string cookingSkillLevel)
        {
            Debug.WriteLine($"ResolveCookingSkillIdentifier called with cooking skill level: {cookingSkillLevel}");
            return cookingSkillLevel?.ToLower() switch
            {
                "beginner" => BeginnerSkillId,
                "intermediate" => IntermediateSkillId,
                "advanced" => AdvancedSkillId,
                _ => DefaultCookingSkillId
            };
        }
    }
}
