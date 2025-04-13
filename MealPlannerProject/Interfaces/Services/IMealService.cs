namespace MealPlannerProject.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MealPlannerProject.Models;

namespace MealPlannerProject.Interfaces.Services
{
    public interface IMealService
    {
        Task<List<Meal>> GetAllMealsAsync();
        Task<bool> CreateMealAsync(Meal meal, string cookingLevel);
        Task<Ingredient> GetIngredientByNameAsync(string name);
        Task<int> CreateMealAsync(Meal meal);
        Task<bool> AddMealIngredientAsync(int mealId, int ingredientId, float quantity);
    }
}
