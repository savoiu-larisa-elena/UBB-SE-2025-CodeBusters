using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MealPlanner.Models;
using MealPlanner.Services;
using MealPlanner.Pages;

namespace MealPlanner.ViewModels
{
    public class MealListPageViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;

        public ObservableCollection<Meal> SelectedMeals { get; private set; }
        public ObservableCollection<Meal> AllMeals { get; }
        public ObservableCollection<string> RecentMeals { get; }
        public ObservableCollection<string> FavoriteMeals { get; }

        public ICommand SelectCategoryCommand { get; }
        public ICommand BackCommand { get; }

        public MealListPageViewModel()
        {
            _navigationService = NavigationService.Instance;

            // Mock Meals Data
            AllMeals = new ObservableCollection<Meal>
            {
                new Meal { Name = "Tomato and Feta Baked Eggs", Category = "Breakfast", ImagePath="Assets/meals/breakfast1.png" },
                new Meal { Name = "Loaded Pancake Tacos", Category = "Lunch", ImagePath="Assets/meals/lunch1.png" },
                new Meal { Name = "Rice With Salmon and Broccoli", Category = "Dinner", ImagePath="Assets/meals/dinner1.png" },
                new Meal { Name = "Air Fryer Strawberry-Thyme Scones", Category = "Snack", ImagePath="Assets/meals/snack1.png" },
                new Meal { Name = "Sweet Potato Breakfast Burritos", Category = "Breakfast", ImagePath="Assets/meals/breakfast2.png" },
                new Meal { Name = "Cheesy Mushroom Pizza", Category = "Dinner", ImagePath="Assets/meals/dinner2.png" }
            };

            SelectedMeals = new ObservableCollection<Meal>(AllMeals);

            // Mock Recent Meals
            RecentMeals = new ObservableCollection<string>
            {
                "Grilled Chicken Salad (350 kcal)",
                "Vegetable Stir-Fry with Tofu (400 kcal)",
                "Lentil Soup (250 kcal)",
                "Veggie Omelette (300 kcal)",
                "Chickpea and Spinach Curry (368 kcal)",
                "Turkey and Avocado Wrap (421 kcal)",
                "Baked Cod with Asparagus (334 kcal)",
                "Butternut Squash Soup (224 kcal)"
            };

            // Mock Favorite Meals
            FavoriteMeals = new ObservableCollection<string>
            {
                "Cabbage and Carrot Slaw (128 kcal)",
                "Eggplant Parmesan (Baked) (356 kcal)",
                "Mango and Black Bean Salad (223 kcal)",
                "Moroccan Chickpea Stew (311 kcal)"
            };

            // Command for filtering meals by category
            SelectCategoryCommand = new RelayCommand(() => FilterMeals());

            // Back Command
            BackCommand = new RelayCommand(() => _navigationService.GoBack());
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                    FilterMeals();
                }
            }
        }

        private void FilterMeals()
        {
            if (!string.IsNullOrEmpty(_selectedCategory))
            {
                SelectedMeals = new ObservableCollection<Meal>(AllMeals.Where(m => m.Category == _selectedCategory));
                OnPropertyChanged(nameof(SelectedMeals));
            }
        }

        public void SelectMeal(Meal meal)
        {
            if (meal != null)
            {
                _navigationService.NavigateTo(typeof(MealDisplayPage));
            }
        }
    }
}