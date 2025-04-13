namespace MealPlannerProject.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using MealPlannerProject.Models;

    public class MealViewModel : INotifyPropertyChanged
    {
        private string mealName = string.Empty; // Initialize to avoid null
        private string cookingTime = string.Empty; // Initialize to avoid null
        private ObservableCollection<string> directions = new ObservableCollection<string>(); // Initialize to avoid null
        private ObservableCollection<string> ingredients = new ObservableCollection<string>(); // Initialize to avoid null
        private int calories;
        private int protein;
        private int carbs;
        private int fat;
        private int fiber;
        private int sugar;

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

        public ObservableCollection<string> Directions
        {
            get => this.directions;
            set
            {
                if (this.directions != value)
                {
                    this.directions = value;
                    Debug.WriteLine($"Directions set with {value?.Count ?? 0} items");
                    this.OnPropertyChanged(nameof(this.Directions));
                }
            }
        }

        public ObservableCollection<string> Ingredients
        {
            get => this.ingredients;
            set
            {
                if (this.ingredients != value)
                {
                    this.ingredients = value;
                    Debug.WriteLine($"Ingredients set with {value?.Count ?? 0} items");
                    this.OnPropertyChanged(nameof(this.Ingredients));
                }
            }
        }

        public int Calories
        {
            get => this.calories;
            set
            {
                if (this.calories != value)
                {
                    this.calories = value;
                    Debug.WriteLine($"Calories set to: {value}");
                    this.OnPropertyChanged(nameof(this.Calories));
                }
            }
        }

        public int Protein
        {
            get => this.protein;
            set
            {
                if (this.protein != value)
                {
                    this.protein = value;
                    Debug.WriteLine($"Protein set to: {value}");
                    this.OnPropertyChanged(nameof(this.Protein));
                }
            }
        }

        public int Carbs
        {
            get => this.carbs;
            set
            {
                if (this.carbs != value)
                {
                    this.carbs = value;
                    Debug.WriteLine($"Carbs set to: {value}");
                    this.OnPropertyChanged(nameof(this.Carbs));
                }
            }
        }

        public int Fat
        {
            get => this.fat;
            set
            {
                if (this.fat != value)
                {
                    this.fat = value;
                    Debug.WriteLine($"Fat set to: {value}");
                    this.OnPropertyChanged(nameof(this.Fat));
                }
            }
        }

        public int Fiber
        {
            get => this.fiber;
            set
            {
                if (this.fiber != value)
                {
                    this.fiber = value;
                    Debug.WriteLine($"Fiber set to: {value}");
                    this.OnPropertyChanged(nameof(this.Fiber));
                }
            }
        }

        public int Sugar
        {
            get => this.sugar;
            set
            {
                if (this.sugar != value)
                {
                    this.sugar = value;
                    Debug.WriteLine($"Sugar set to: {value}");
                    this.OnPropertyChanged(nameof(this.Sugar));
                }
            }
        }

        public MealViewModel()
        {
            Debug.WriteLine("Initializing MealViewModel");
            this.Directions = new ObservableCollection<string>();
            this.Ingredients = new ObservableCollection<string>();
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
            this.Calories = meal.Calories;
            this.Protein = meal.Protein;
            this.Carbs = meal.Carbohydrates;
            this.Fat = meal.Fat;
            this.Fiber = meal.Fiber;
            this.Sugar = meal.Sugar;

            // Initialize Ingredients
            if (!string.IsNullOrEmpty(meal.Ingredients))
            {
                this.Ingredients = new ObservableCollection<string>(meal.Ingredients.Split('\n'));
                Debug.WriteLine($"Loaded {this.Ingredients.Count} ingredients");
            }
            else
            {
                Debug.WriteLine("No ingredients found in meal");
                this.Ingredients = new ObservableCollection<string> { "No ingredients available" };
            }

            // Initialize Directions
            if (!string.IsNullOrEmpty(meal.Recipe))
            {
                this.Directions = new ObservableCollection<string>(meal.Recipe.Split('\n'));
                Debug.WriteLine($"Loaded {this.Directions.Count} directions");
            }
            else
            {
                Debug.WriteLine("No recipe found in meal");
                this.Directions = new ObservableCollection<string> { "No directions available" };
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
