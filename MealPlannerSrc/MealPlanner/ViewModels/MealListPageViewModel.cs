using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MealPlanner.Models;
using MealPlanner.Services;
using MealPlanner.Pages;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;

namespace MealPlanner.ViewModels
{
    public class MealListPageViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;
        private readonly MealService _mealService;

        public ObservableCollection<Meal> SelectedMeals { get; private set; }
        public ObservableCollection<Meal> AllMeals { get; private set; }
        public ObservableCollection<string> RecentMeals { get; private set; }
        public ObservableCollection<string> FavoriteMeals { get; private set; }

        public ICommand SelectCategoryCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand SelectMealCommand { get; }
        public ICommand RefreshCommand { get; }

        public MealListPageViewModel()
        {
            _navigationService = NavigationService.Instance;
            _mealService = new MealService();

            // Initialize collections
            AllMeals = new ObservableCollection<Meal>();
            SelectedMeals = new ObservableCollection<Meal>();
            RecentMeals = new ObservableCollection<string>();
            FavoriteMeals = new ObservableCollection<string>();

            // Initialize commands
            SelectCategoryCommand = new RelayCommand<string>(OnSelectCategory);
            BackCommand = new RelayCommand(() => _navigationService.GoBack());
            SelectMealCommand = new RelayCommand<Meal>(SelectMeal);
            RefreshCommand = new RelayCommand(async () => await LoadMealsAsync());

            // Load meals from database
            _ = LoadMealsAsync();
        }

        public async Task LoadMealsAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Starting to load meals...");
                var meals = await _mealService.GetAllMealsAsync();
                System.Diagnostics.Debug.WriteLine($"Got {meals?.Count ?? 0} meals from service");
                
                if (meals != null && meals.Any())
                {
                    // Use database meals
                    AllMeals.Clear();
                    foreach (var meal in meals)
                    {
                        AllMeals.Add(meal);
                        System.Diagnostics.Debug.WriteLine($"Added meal to AllMeals: {meal.Name}");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No meals found in database, using mock data");
                    // Use mock data
                    AllMeals.Clear();
                    var mockMeals = new List<Meal>
                    {
                        new Meal { Name = "Grilled Chicken Salad", Category = "Lunch", Calories = 350, Protein = 30, Carbohydrates = 15, Fat = 5, Fiber = 6, Sugar = 3, PhotoLink = "Assets/meals/chicken_salad.jpg" },
                        new Meal { Name = "Oatmeal with Berries", Category = "Breakfast", Calories = 220, Protein = 8, Carbohydrates = 35, Fat = 3, Fiber = 4, Sugar = 8, PhotoLink = "Assets/meals/oatmeal.jpg" },
                        new Meal { Name = "Salmon with Quinoa", Category = "Dinner", Calories = 500, Protein = 40, Carbohydrates = 35, Fat = 12, Fiber = 5, Sugar = 2, PhotoLink = "Assets/meals/salmon.jpg" },
                        new Meal { Name = "Vegan Stir Fry", Category = "Dinner", Calories = 320, Protein = 20, Carbohydrates = 45, Fat = 6, Fiber = 7, Sugar = 5, PhotoLink = "Assets/meals/stir_fry.jpg" },
                        new Meal { Name = "Protein Pancakes", Category = "Breakfast", Calories = 290, Protein = 25, Carbohydrates = 30, Fat = 8, Fiber = 4, Sugar = 3, PhotoLink = "Assets/meals/pancakes.jpg" }
                    };

                    foreach (var meal in mockMeals)
                    {
                        AllMeals.Add(meal);
                        System.Diagnostics.Debug.WriteLine($"Added mock meal to AllMeals: {meal.Name}");
                    }
                }

                System.Diagnostics.Debug.WriteLine($"Total meals in AllMeals: {AllMeals.Count}");

                // Update selected meals based on current category filter
                if (!string.IsNullOrEmpty(_selectedCategory))
                {
                    SelectedMeals = new ObservableCollection<Meal>(AllMeals.Where(m => m.Category == _selectedCategory));
                    System.Diagnostics.Debug.WriteLine($"Filtered meals for category {_selectedCategory}: {SelectedMeals.Count}");
                }
                else
                {
                    SelectedMeals = new ObservableCollection<Meal>(AllMeals);
                    System.Diagnostics.Debug.WriteLine($"Showing all meals: {SelectedMeals.Count}");
                }
                OnPropertyChanged(nameof(SelectedMeals));

                // Update recent meals (last 8 meals)
                RecentMeals.Clear();
                foreach (var meal in AllMeals.Take(8))
                {
                    RecentMeals.Add($"{meal.Name} ({meal.Calories} kcal)");
                }

                // Update favorite meals (meals with high protein content)
                FavoriteMeals.Clear();
                foreach (var meal in AllMeals.Where(m => m.Protein > 25).Take(4))
                {
                    FavoriteMeals.Add($"{meal.Name} ({meal.Protein}g protein)");
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading meals: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                // Show empty collections on error
                AllMeals.Clear();
                SelectedMeals.Clear();
                RecentMeals.Clear();
                FavoriteMeals.Clear();
            }
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

        public void OnSelectCategory(string? category)
        {
            if (category != null)
            {
                SelectedCategory = category;
            }
        }

        private void FilterMeals()
        {
            if (!string.IsNullOrEmpty(_selectedCategory))
            {
                SelectedMeals = new ObservableCollection<Meal>(AllMeals.Where(m => m.Category == _selectedCategory));
            }
            else
            {
                SelectedMeals = new ObservableCollection<Meal>(AllMeals);
            }
            OnPropertyChanged(nameof(SelectedMeals));
        }

        public void SelectMeal(Meal? meal)
        {
            if (meal != null)
            {
                _navigationService.NavigateTo(typeof(MealDisplayPage));
            }
        }
    }
}