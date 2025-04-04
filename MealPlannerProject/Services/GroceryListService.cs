﻿using MealPlannerProject.Models;
using MealPlannerProject.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Services
{
    internal class GroceryListService
    {
        public List<GroceryIngredient> GetIngredientsForUser(int userId)
        {
            var ingredients = new List<GroceryIngredient>();

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = userId }
            };

            DataTable table = DataLink.Instance.ExecuteReader("sp_GetUserGroceryList", parameters);

            foreach (DataRow row in table.Rows)
            {
                ingredients.Add(new GroceryIngredient
                {
                    Id = (int)row["i_id"],
                    Name = row["i_name"].ToString(),
                    IsChecked = (bool)row["is_checked"]
                });
            }

            return ingredients;
        }

        public void UpdateIsChecked(int userId, int ingredientId, bool isChecked)
        {
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = userId },
                new SqlParameter("@IngredientId", SqlDbType.Int) { Value = ingredientId },
                new SqlParameter("@IsChecked", SqlDbType.Bit) { Value = isChecked }
            };

            DataLink.Instance.ExecuteNonQuery("sp_UpdateGroceryItemChecked", parameters);
        }


        public void AddIngredientToUser(int userId, GroceryIngredient ingredient)
        {
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = userId },
                new SqlParameter("@IngredientName", SqlDbType.NVarChar) { Value = ingredient.Name }
            };

            int newId = DataLink.Instance.ExecuteScalar<int>("sp_AddIngredientToUserList", parameters, true);
            ingredient.Id = newId;
        }


    }
}
