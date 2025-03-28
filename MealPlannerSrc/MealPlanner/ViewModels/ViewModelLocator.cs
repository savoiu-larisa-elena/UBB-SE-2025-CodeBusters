using MealPlanner.ViewModels;

namespace MealPlanner.ViewModels
{
    public class ViewModelLocator
    {
        public GoalViewModel GoalViewModel { get; } = new GoalViewModel();

        public WelcomeViewModel WelcomeViewModel { get; } = new WelcomeViewModel();

        public CreateMealViewModel CreateMealViewModel { get; } = new CreateMealViewModel();

        public BodyMetricsViewModel BodyMetricsViewModel { get; } = new BodyMetricsViewModel();

        public ActivityLevelViewModel ActivityLevelViewModel { get; } = new ActivityLevelViewModel();

        public GroceryViewModel GroceryViewModel { get; } = new GroceryViewModel();

        public AddFoodPageViewModel AddFoodViewModel { get; } = new AddFoodPageViewModel();

        public MealListPageViewModel MealListViewModel { get; } = new MealListPageViewModel();


    }
}
