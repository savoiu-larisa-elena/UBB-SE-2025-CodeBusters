namespace MealPlannerProject.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using MealPlannerProject.Models;

    public class MealViewModel : INotifyPropertyChanged
    {
        private string mealName = string.Empty;
        private string cookingTime = string.Empty;
        private ObservableCollection<string> cookingDirections = new ObservableCollection<string>();
        private ObservableCollection<string> mealIngredients = new ObservableCollection<string>();
        private int caloriesGrams;
        private int proteinGrams;
        private int carbohydrateGrams;
        private int fatGrams;
        private int fiberGrams;
        private int sugarGrams;

        public string MealName
        {
            get => this.mealName;
            set
            {
                if (this.mealName != value)
                {
                    this.mealName = value;
                    Debug.WriteLine($"MealName set to: {value}");
                    this.OnPropertyChanged(nameof(this.MealName));
                }
            }
        }

        public string CookingTime
        {
            get => this.cookingTime;
            set
            {
                if (this.cookingTime != value)
                {
                    this.cookingTime = value;
                    Debug.WriteLine($"CookingTime set to: {value}");
                    this.OnPropertyChanged(nameof(this.CookingTime));
                }
            }
        }

        public ObservableCollection<string> CookingDirections
        {
            get => this.cookingDirections;
            set
            {
                if (this.cookingDirections != value)
                {
                    this.cookingDirections = value;
                    Debug.WriteLine($"Directions set with {value?.Count ?? 0} items");
                    this.OnPropertyChanged(nameof(this.CookingDirections));
                }
            }
        }

        public ObservableCollection<string> MealIngredients
        {
            get => this.mealIngredients;
            set
            {
                if (this.mealIngredients != value)
                {
                    this.mealIngredients = value;
                    Debug.WriteLine($"Ingredients set with {value?.Count ?? 0} items");
                    this.OnPropertyChanged(nameof(this.MealIngredients));
                }
            }
        }

        public int CalorieCount
        {
            get => this.caloriesGrams;
            set
            {
                if (this.caloriesGrams != value)
                {
                    this.caloriesGrams = value;
                    Debug.WriteLine($"Calories set to: {value}");
                    this.OnPropertyChanged(nameof(this.CalorieCount));
                }
            }
        }

        public int ProteinGrams
        {
            get => this.proteinGrams;
            set
            {
                if (this.proteinGrams != value)
                {
                    this.proteinGrams = value;
                    Debug.WriteLine($"Protein set to: {value}");
                    this.OnPropertyChanged(nameof(this.ProteinGrams));
                }
            }
        }

        public int CarbohydrateGrams
        {
            get => this.carbohydrateGrams;
            set
            {
                if (this.carbohydrateGrams != value)
                {
                    this.carbohydrateGrams = value;
                    Debug.WriteLine($"Carbs set to: {value}");
                    this.OnPropertyChanged(nameof(this.CarbohydrateGrams));
                }
            }
        }

        public int FatGrams
        {
            get => this.fatGrams;
            set
            {
                if (this.fatGrams != value)
                {
                    this.fatGrams = value;
                    Debug.WriteLine($"Fat set to: {value}");
                    this.OnPropertyChanged(nameof(this.FatGrams));
                }
            }
        }

        public int FiberGrams
        {
            get => this.fiberGrams;
            set
            {
                if (this.fiberGrams != value)
                {
                    this.fiberGrams = value;
                    Debug.WriteLine($"Fiber set to: {value}");
                    this.OnPropertyChanged(nameof(this.FiberGrams));
                }
            }
        }

        public int SugarGrams
        {
            get => this.sugarGrams;
            set
            {
                if (this.sugarGrams != value)
                {
                    this.sugarGrams = value;
                    Debug.WriteLine($"Sugar set to: {value}");
                    this.OnPropertyChanged(nameof(this.SugarGrams));
                }
            }
        }

        public MealViewModel()
        {
            Debug.WriteLine("Initializing MealViewModel");
            this.CookingDirections = new ObservableCollection<string>();
            this.MealIngredients = new ObservableCollection<string>();
        }

        public void InitializeFromMeal(Meal meal)
        {
            Debug.WriteLine($"Initializing MealViewModel from Meal: {meal?.Name ?? "null"}");

            if (meal == null)
            {
                Debug.WriteLine("Warning: Meal object is null");
                return;
            }

            this.MealName = meal.Name;
            this.CookingTime = $"{meal.PreparationTime} min";
            this.CalorieCount = meal.Calories;
            this.ProteinGrams = meal.Protein;
            this.CarbohydrateGrams = meal.Carbohydrates;
            this.FatGrams = meal.Fat;
            this.FiberGrams = meal.Fiber;
            this.SugarGrams = meal.Sugar;

            if (!string.IsNullOrEmpty(meal.Ingredients))
            {
                this.MealIngredients = new ObservableCollection<string>(meal.Ingredients.Split('\n'));
                Debug.WriteLine($"Loaded {this.MealIngredients.Count} ingredients");
            }
            else
            {
                Debug.WriteLine("No ingredients found in meal");
                this.MealIngredients = new ObservableCollection<string> { "No ingredients available" };
            }

            if (!string.IsNullOrEmpty(meal.Recipe))
            {
                this.CookingDirections = new ObservableCollection<string>(meal.Recipe.Split('\n'));
                Debug.WriteLine($"Loaded {this.CookingDirections.Count} directions");
            }
            else
            {
                Debug.WriteLine("No recipe found in meal");
                this.CookingDirections = new ObservableCollection<string> { "No directions available" };
            }

            Debug.WriteLine("Finished initializing MealViewModel from Meal");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
