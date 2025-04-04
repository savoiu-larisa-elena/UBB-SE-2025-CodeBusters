using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MealPlannerProject.Models;

namespace MealPlannerProject.ViewModels
{
    public class MealViewModel : INotifyPropertyChanged
    {
        private string mealName;
        private string cookingTime;
        private ObservableCollection<string> directions;
        private ObservableCollection<string> ingredients;
        private int calories;
        private int protein;
        private int carbs;
        private int fat;
        private int fiber;
        private int sugar;

        public string MealName
        {
            get => mealName;
            set
            {
                if (mealName != value)
                {
                    mealName = value;
                    Debug.WriteLine($"MealName set to: {value}");
                    OnPropertyChanged(nameof(MealName));
                }
            }
        }

        public string CookingTime
        {
            get => cookingTime;
            set
            {
                if (cookingTime != value)
                {
                    cookingTime = value;
                    Debug.WriteLine($"CookingTime set to: {value}");
                    OnPropertyChanged(nameof(CookingTime));
                }
            }
        }

        public ObservableCollection<string> Directions
        {
            get => directions;
            set
            {
                if (directions != value)
                {
                    directions = value;
                    Debug.WriteLine($"Directions set with {value?.Count ?? 0} items");
                    OnPropertyChanged(nameof(Directions));
                }
            }
        }

        public ObservableCollection<string> Ingredients
        {
            get => ingredients;
            set
            {
                if (ingredients != value)
                {
                    ingredients = value;
                    Debug.WriteLine($"Ingredients set with {value?.Count ?? 0} items");
                    OnPropertyChanged(nameof(Ingredients));
                }
            }
        }

        public int Calories
        {
            get => calories;
            set
            {
                if (calories != value)
                {
                    calories = value;
                    Debug.WriteLine($"Calories set to: {value}");
                    OnPropertyChanged(nameof(Calories));
                }
            }
        }

        public int Protein
        {
            get => protein;
            set
            {
                if (protein != value)
                {
                    protein = value;
                    Debug.WriteLine($"Protein set to: {value}");
                    OnPropertyChanged(nameof(Protein));
                }
            }
        }

        public int Carbs
        {
            get => carbs;
            set
            {
                if (carbs != value)
                {
                    carbs = value;
                    Debug.WriteLine($"Carbs set to: {value}");
                    OnPropertyChanged(nameof(Carbs));
                }
            }
        }

        public int Fat
        {
            get => fat;
            set
            {
                if (fat != value)
                {
                    fat = value;
                    Debug.WriteLine($"Fat set to: {value}");
                    OnPropertyChanged(nameof(Fat));
                }
            }
        }

        public int Fiber
        {
            get => fiber;
            set
            {
                if (fiber != value)
                {
                    fiber = value;
                    Debug.WriteLine($"Fiber set to: {value}");
                    OnPropertyChanged(nameof(Fiber));
                }
            }
        }

        public int Sugar
        {
            get => sugar;
            set
            {
                if (sugar != value)
                {
                    sugar = value;
                    Debug.WriteLine($"Sugar set to: {value}");
                    OnPropertyChanged(nameof(Sugar));
                }
            }
        }


        public MealViewModel()
        {
            Debug.WriteLine("Initializing MealViewModel");
            Directions = new ObservableCollection<string>();
            Ingredients = new ObservableCollection<string>();
        }

        public void InitializeFromMeal(Meal meal)
        {
            Debug.WriteLine($"Initializing MealViewModel from Meal: {meal?.Name ?? "null"}");

            if (meal == null)
            {
                Debug.WriteLine("Warning: Meal object is null");
                return;
            }

            MealName = meal.Name;
            CookingTime = $"{meal.PreparationTime} min";
            Calories = meal.Calories;
            Protein = meal.Protein;
            Carbs = meal.Carbohydrates;
            Fat = meal.Fat;
            Fiber = meal.Fiber;
            Sugar = meal.Sugar;

            // Initialize Ingredients
            if (!string.IsNullOrEmpty(meal.Ingredients))
            {
                Ingredients = new ObservableCollection<string>(meal.Ingredients.Split('\n'));
                Debug.WriteLine($"Loaded {Ingredients.Count} ingredients");
            }
            else
            {
                Debug.WriteLine("No ingredients found in meal");
                Ingredients = new ObservableCollection<string> { "No ingredients available" };
            }

            // Initialize Directions
            if (!string.IsNullOrEmpty(meal.Recipe))
            {
                Directions = new ObservableCollection<string>(meal.Recipe.Split('\n'));
                Debug.WriteLine($"Loaded {Directions.Count} directions");
            }
            else
            {
                Debug.WriteLine("No recipe found in meal");
                Directions = new ObservableCollection<string> { "No directions available" };
            }

            Debug.WriteLine("Finished initializing MealViewModel from Meal");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
