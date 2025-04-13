using MealPlannerProject.Models;
using System.Threading.Tasks;

namespace MealPlannerProject.Interfaces
{
    public interface IIngredientRepository
    {
        // Async method to fetch ingredient details by name
        Task<Ingredient?> GetIngredientByNameAsync(string name);
    }
}
