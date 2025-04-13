using System;

namespace MealPlannerProject.Interfaces
{
    public interface INavigationService
    {
        void NavigateTo(Type pageType, object parameter = null);
        void GoBack();
    }
}
