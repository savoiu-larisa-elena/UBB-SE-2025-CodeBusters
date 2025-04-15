namespace MealPlannerProject.Interfaces.Services
{
    internal interface IDietaryPreferencesService
    {
        void AddAllergyAndDietaryPreference(string firstName, string lastName, string dietaryPreference, string allergy);
    }
}
