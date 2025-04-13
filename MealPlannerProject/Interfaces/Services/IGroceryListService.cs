using MealPlannerProject.Models;
using System.Collections.Generic;

public interface IGroceryListService
{
    List<GroceryIngredient> GetIngredientsForUser(int userId);
    void UpdateIsChecked(int userId, int ingredientId, bool isChecked);
    void AddIngredientToUser(int userId, GroceryIngredient ingredient);
}
