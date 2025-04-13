using System.Collections.ObjectModel;
using System.Windows.Input;
using MealPlannerProject.Models;

namespace MealPlannerProject.Interfaces
{
    public interface IMealListViewModel
    {
        ObservableCollection<Meal> AllMeals { get; }
        ObservableCollection<Meal> BreakfastMeals { get; }
        ObservableCollection<Meal> LunchMeals { get; }
        ObservableCollection<Meal> DinnerMeals { get; }
        ObservableCollection<Meal> SnackMeals { get; }
        ObservableCollection<string> RecentMeals { get; }
        ObservableCollection<string> FavoriteMeals { get; }

        Meal SelectedMeal { get; set; }

        ICommand BackCommand { get; }
        ICommand SelectMealCommand { get; }

        void NavigateBack();
    }
}
