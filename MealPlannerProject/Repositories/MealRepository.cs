﻿using MealPlannerProject.Models;
using MealPlannerProject.Queries;
using System;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MealPlannerProject.Repositories
{
    public class MealRepository
    {
        public async Task<int> CreateMealAsync(Meal meal, int cookingSkillId, int mealTypeId)
        {
            string query = @"INSERT INTO meals (m_name, recipe, cs_id, mt_id, preparation_time, servings, protein, calories, carbohydrates, fat, fiber, sugar, photo_link) 
                               VALUES (@m_name, @recipe, @cs_id, @mt_id, @preparation_time, @servings, @protein, @calories, @carbohydrates, @fat, @fiber, @sugar, @photo_link); 
                               SELECT SCOPE_IDENTITY();";

            var parameters = new SqlParameter[]
            {
                    new("@m_name", meal.Name),
                    new("@recipe", meal.Recipe ?? "No directions provided"),
                    new("@cs_id", cookingSkillId),
                    new("@mt_id", mealTypeId),
                    new("@preparation_time", meal.PreparationTime),
                    new("@servings", meal.Servings),
                    new("@protein", meal.Protein),
                    new("@calories", meal.Calories),
                    new("@carbohydrates", meal.Carbohydrates),
                    new("@fat", meal.Fat),
                    new("@fiber", meal.Fiber),
                    new("@sugar", meal.Sugar),
                    new("@photo_link", meal.PhotoLink ?? "default.jpg")
            };

            return await Task.FromResult(DataLink.Instance.ExecuteScalar<int>(query, parameters, false));
        }

        public async Task<int> AddMealIngredientAsync(int mealId, int ingredientId, float quantity)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@mealId", mealId),
                new SqlParameter("@ingredientId", ingredientId),
                new SqlParameter("@quantity", quantity)
            };

            return await DataLink.Instance.ExecuteNonQueryAsync("InsertMealIngredient", parameters);
        }

    }
}
