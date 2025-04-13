namespace MealPlannerProject.Interfaces.Services
{
    internal interface IWaterService
    {
        float GetWaterIntake(int userId);
        void RemoveWater300(int userId);
        void RemoveWater400(int userId);
        void RemoveWater500(int userId);
        void RemoveWater750(int userId);
        void UpdateWaterIntake(int userId, float newIntake);
    }
}