using MealPlannerProject.Pages;
using MealPlannerProject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data.SqlClient;
using MealPlannerProject.ViewModels;
using MealPlannerProject.Queries;
using System.Data;

namespace MealPlannerProject.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Generic method to update properties
        private string _circleText;
        private string _baseGoalCal;
        private string _foodCal;
        private string _exerciseCal;
        private string _waterPercent;
        private string _waterGoal;

        private string _totalPr;
        private string _goalPr;
        private string _leftPr;

        private string _totalCarb;
        private string _goalCarb;
        private string _leftCarb;

        private string _totalFib;
        private string _goalFib;
        private string _leftFib;

        private string _totalFat;
        private string _goalFat;
        private string _leftFat;

        private string _totalSug;
        private string _goalSug;
        private string _leftSug;

        public string CircleText
        {
            get => _circleText;
            set => SetProperty(ref _circleText, value);
        }

        public string BaseGoal_Cal
        {
            get => _baseGoalCal;
            set => SetProperty(ref _baseGoalCal, value);
        }

        public string Food_Cal
        {
            get => _foodCal;
            set => SetProperty(ref _foodCal, value);
        }

        public string Exercise_Cal
        {
            get => _exerciseCal;
            set => SetProperty(ref _exerciseCal, value);
        }

        public string TotalPr
        {
            get => _totalPr;
            set => SetProperty(ref _totalPr, value);
        }

        public string GoalPr
        {
            get => _goalPr;
            set => SetProperty(ref _goalPr, value);
        }

        public string LeftPr
        {
            get => _leftPr;
            set => SetProperty(ref _leftPr, value);
        }

        public string TotalCarb { get => _totalCarb; set => SetProperty(ref _totalCarb, value); }
        public string GoalCarb { get => _goalCarb; set => SetProperty(ref _goalCarb, value); }
        public string LeftCarb { get => _leftCarb; set => SetProperty(ref _leftCarb, value); }

        public string TotalFib { get => _totalFib; set => SetProperty(ref _totalFib, value); }
        public string GoalFib { get => _goalFib; set => SetProperty(ref _goalFib, value); }
        public string LeftFib { get => _leftFib; set => SetProperty(ref _leftFib, value); }

        public string TotalFat { get => _totalFat; set => SetProperty(ref _totalFat, value); }
        public string GoalFat { get => _goalFat; set => SetProperty(ref _goalFat, value); }
        public string LeftFat { get => _leftFat; set => SetProperty(ref _leftFat, value); }

        public string TotalSug { get => _totalSug; set => SetProperty(ref _totalSug, value); }
        public string GoalSug { get => _goalSug; set => SetProperty(ref _goalSug, value); }
        public string LeftSug { get => _leftSug; set => SetProperty(ref _leftSug, value); }

        public string WaterPercent { get => _waterPercent; set => SetProperty(ref _waterPercent, value); }
        public string WaterGoal { get => _waterGoal; set => SetProperty(ref _waterGoal, value); }

        // Define properties for R1 to R6
        private string[] _rValues = new string[36];
        public string R1 { get => _rValues[0]; set => SetProperty(ref _rValues[0], value); }
        public string R1Cal { get => _rValues[1]; set => SetProperty(ref _rValues[1], value); }
        public string R1Diet { get => _rValues[2]; set => SetProperty(ref _rValues[2], value); }
        public string R1Level { get => _rValues[3]; set => SetProperty(ref _rValues[3], value); }
        public string R1Time { get => _rValues[4]; set => SetProperty(ref _rValues[4], value); }
        public string R1MealType { get => _rValues[5]; set => SetProperty(ref _rValues[5], value); }

        public string R2 { get => _rValues[6]; set => SetProperty(ref _rValues[6], value); }
        public string R2Cal { get => _rValues[7]; set => SetProperty(ref _rValues[7], value); }
        public string R2Diet { get => _rValues[8]; set => SetProperty(ref _rValues[8], value); }
        public string R2Level { get => _rValues[9]; set => SetProperty(ref _rValues[9], value); }
        public string R2Time { get => _rValues[10]; set => SetProperty(ref _rValues[10], value); }
        public string R2MealType { get => _rValues[11]; set => SetProperty(ref _rValues[11], value); }

        public string R3 { get => _rValues[12]; set => SetProperty(ref _rValues[12], value); }
        public string R3Cal { get => _rValues[13]; set => SetProperty(ref _rValues[13], value); }
        public string R3Diet { get => _rValues[14]; set => SetProperty(ref _rValues[14], value); }
        public string R3Level { get => _rValues[15]; set => SetProperty(ref _rValues[15], value); }
        public string R3Time { get => _rValues[16]; set => SetProperty(ref _rValues[16], value); }
        public string R3MealType { get => _rValues[17]; set => SetProperty(ref _rValues[17], value); }

        public string R4 { get => _rValues[18]; set => SetProperty(ref _rValues[18], value); }
        public string R4Cal { get => _rValues[19]; set => SetProperty(ref _rValues[19], value); }
        public string R4Diet { get => _rValues[20]; set => SetProperty(ref _rValues[20], value); }
        public string R4Level { get => _rValues[21]; set => SetProperty(ref _rValues[21], value); }
        public string R4Time { get => _rValues[22]; set => SetProperty(ref _rValues[22], value); }
        public string R4MealType { get => _rValues[23]; set => SetProperty(ref _rValues[23], value); }

        public string R5 { get => _rValues[24]; set => SetProperty(ref _rValues[24], value); }
        public string R5Cal { get => _rValues[25]; set => SetProperty(ref _rValues[25], value); }
        public string R5Diet { get => _rValues[26]; set => SetProperty(ref _rValues[26], value); }
        public string R5Level { get => _rValues[27]; set => SetProperty(ref _rValues[27], value); }
        public string R5Time { get => _rValues[28]; set => SetProperty(ref _rValues[28], value); }
        public string R5MealType { get => _rValues[29]; set => SetProperty(ref _rValues[29], value); }

        public string R6 { get => _rValues[30]; set => SetProperty(ref _rValues[30], value); }
        public string R6Cal { get => _rValues[31]; set => SetProperty(ref _rValues[31], value); }
        public string R6Diet { get => _rValues[32]; set => SetProperty(ref _rValues[32], value); }
        public string R6Level { get => _rValues[33]; set => SetProperty(ref _rValues[33], value); }
        public string R6Time { get => _rValues[34]; set => SetProperty(ref _rValues[34], value); }
        public string R6MealType { get => _rValues[35]; set => SetProperty(ref _rValues[35], value); }

        public ICommand RefreshCommand { get; }

        // Generic property setter method
        private void SetProperty(ref string field, string value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        // Commands -------------------------------------
        public ICommand RefreshCircleTextCommand { get; }
        public ICommand RefreshBaseGoalTextCommand { get; }
        public ICommand RefreshFoodTextCommand { get; }
        public ICommand RefreshExerciseTextCommand { get; }

        public ICommand RefreshTotalPrTextCommand { get; }
        public ICommand RefreshGoalPrTextCommand { get; }
        public ICommand RefreshLeftPrTextCommand { get; }

        public ICommand RefreshTotalCarbTextCommand { get; }
        public ICommand RefreshGoalCarbTextCommand { get; }
        public ICommand RefreshLeftCarbTextCommand { get; }

        public ICommand RefreshTotalFibTextCommand { get; }
        public ICommand RefreshGoalFibTextCommand { get; }
        public ICommand RefreshLeftFibTextCommand { get; }

        public ICommand RefreshTotalFatTextCommand { get; }
        public ICommand RefreshGoalFatTextCommand { get; }
        public ICommand RefreshLeftFatTextCommand { get; }

        public ICommand RefreshTotalSugTextCommand { get; }
        public ICommand RefreshGoalSugTextCommand { get; }
        public ICommand RefreshLeftSugTextCommand { get; }

        public ICommand NavigateCommand { get; }

        public ICommand DisplayMeals { get; }
        public ICommand CreateMeal { get; }
        public ICommand GroceryList { get; }
        public ICommand AddBreakfast { get; }
        public ICommand AddLunch { get; }
        public ICommand AddDinner { get; }
        public ICommand AddSnack { get; }
        public ICommand AddWater300Command { get; }
        public ICommand AddWater400Command { get; }
        public ICommand AddWater500Command { get; }
        public ICommand AddWater750Command { get; }
        public ICommand RemoveWater300Command { get; }
        public ICommand RemoveWater400Command { get; }
        public ICommand RemoveWater500Command { get; }
        public ICommand RemoveWater750Command { get; }

        // ----------------------------------------

        private readonly WaterService _waterService;
        private readonly CalorieService _calorieService;
        private readonly MacrosService _macrosService;

        private readonly UserPageService _userService;

        private string _userId = "73"; // Replace with actual logged-in user ID

        public ICommand RefreshMealsCommand { get; }

        public MainViewModel()
        {

            int number_userId = int.Parse(_userId);

            // Initialize WaterService
            _waterService = new WaterService();

            // Initialize CalorieService
            _calorieService = new CalorieService();

            // Initialize MacrosService
            _macrosService = new MacrosService();

            System.Diagnostics.Debug.WriteLine("Getting water intake...");
            // Initialize water intake from database
            WaterIntake = _waterService.GetWaterIntake(number_userId).ToString();

            // Initialize water commands
            AddWater300Command = new RelayCommand(() => UpdateWaterIntake(300));
            AddWater400Command = new RelayCommand(() => UpdateWaterIntake(400));
            AddWater500Command = new RelayCommand(() => UpdateWaterIntake(500));
            AddWater750Command = new RelayCommand(() => UpdateWaterIntake(750));
            RemoveWater300Command = new RelayCommand(() => RemoveWaterIntake(300));
            RemoveWater400Command = new RelayCommand(() => RemoveWaterIntake(400));
            RemoveWater500Command = new RelayCommand(() => RemoveWaterIntake(500));
            RemoveWater750Command = new RelayCommand(() => RemoveWaterIntake(750));

            System.Diagnostics.Debug.WriteLine("Loading meals...");
            // Load last 6 meals
            LoadLastMeals(number_userId);

            System.Diagnostics.Debug.WriteLine("Getting food calories...");
            // Initialize calorie values from database
            Food_Cal = _calorieService.GetFood(number_userId).ToString();

            BaseGoal_Cal = "2000";
            Exercise_Cal = "500";

            float circleTextNr = float.Parse(BaseGoal_Cal) - float.Parse(Food_Cal) + float.Parse(Exercise_Cal);
            CircleText = circleTextNr.ToString();

            System.Diagnostics.Debug.WriteLine("Getting macros...");
            // Initialize macros values from database
            TotalPr = _macrosService.GetProteinIntake(number_userId).ToString();
            TotalCarb = _macrosService.GetCarbohydratesIntake(number_userId).ToString();
            TotalFat = _macrosService.GetFatIntake(number_userId).ToString();
            TotalFib = _macrosService.GetFiberIntake(number_userId).ToString();
            TotalSug = _macrosService.GetSugarIntake(number_userId).ToString();

            // Initialize values
            GoalPr = "30";
            GoalCarb = "200";
            GoalFib = "30";
            GoalFat = "90";
            GoalSug = "50";

            LeftPr = (float.Parse(GoalPr) - float.Parse(TotalPr)).ToString();
            LeftCarb = (float.Parse(GoalCarb) - float.Parse(TotalCarb)).ToString();
            LeftFib = (float.Parse(GoalFib) - float.Parse(TotalFib)).ToString();
            LeftFat = (float.Parse(GoalFat) - float.Parse(TotalFat)).ToString();
            LeftSug = (float.Parse(GoalSug) - float.Parse(TotalSug)).ToString();

            WaterGoal = "2000";

            DisplayMeals = new RelayCommand(GoDisplayMeals);

            CreateMeal = new RelayCommand(GoCreateMeal);

            GroceryList = new RelayCommand(GoGroceryList);

            AddBreakfast = new RelayCommand(GoAddBreakfast);

            AddLunch = new RelayCommand(GoAddLunch);

            AddDinner = new RelayCommand(GoAddDinner);

            AddSnack = new RelayCommand(GoAddSnack);

            // Initialize refresh command
            // int number_userId = int.Parse(_userId);
            //RefreshMealsCommand = new RelayCommand(LoadLastMeals);

        }

        public void LoadLastMeals(int userId)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Starting to load last meals...");

                // Initialize all slots with default values first
                string defaultRecipe = "No meals", defaultCal = "0", defaultDiet = "N/A", defaultLevel = "N/A", defaultTime = "N/A", defaultMealType = "N/A";
                R1 = R2 = R3 = R4 = R5 = R6 = defaultRecipe;
                R1Cal = R2Cal = R3Cal = R4Cal = R5Cal = R6Cal = defaultCal;
                R1Diet = R2Diet = R3Diet = R4Diet = R5Diet = R6Diet = defaultDiet;
                R1Level = R2Level = R3Level = R4Level = R5Level = R6Level = defaultLevel;
                R1Time = R2Time = R3Time = R4Time = R5Time = R6Time = defaultTime;
                R1MealType = R2MealType = R3MealType = R4MealType = R5MealType = R6MealType = defaultMealType;

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserId", userId)
                };

                DataTable mealsTable = DataLink.Instance.ExecuteReader("dbo.get_last_6_unique_meals_pr", parameters);

                if (mealsTable.Rows.Count > 0)
                {
                    for (int i = 0; i < mealsTable.Rows.Count && i < 6; i++)
                    {
                        var row = mealsTable.Rows[i];
                        string recipeName = row["RecipeName"]?.ToString() ?? "No Recipe";
                        string calories = row["Calories"]?.ToString() ?? "00";
                        string diet = row["Diet"]?.ToString() ?? "n/a";
                        string level = row["Level"]?.ToString() ?? "n/a";
                        string time = row["Time"]?.ToString() ?? "n/a";
                        string mealType = row["MealType"]?.ToString() ?? "n/a";

                        System.Diagnostics.Debug.WriteLine($"Loading meal {i + 1}: {recipeName}");

                        switch (i)
                        {
                            case 0: R1 = recipeName; R1Cal = calories; R1Diet = diet; R1Level = level; R1Time = time; R1MealType = mealType; break;
                            case 1: R2 = recipeName; R2Cal = calories; R2Diet = diet; R2Level = level; R2Time = time; R2MealType = mealType; break;
                            case 2: R3 = recipeName; R3Cal = calories; R3Diet = diet; R3Level = level; R3Time = time; R3MealType = mealType; break;
                            case 3: R4 = recipeName; R4Cal = calories; R4Diet = diet; R4Level = level; R4Time = time; R4MealType = mealType; break;
                            case 4: R5 = recipeName; R5Cal = calories; R5Diet = diet; R5Level = level; R5Time = time; R5MealType = mealType; break;
                            case 5: R6 = recipeName; R6Cal = calories; R6Diet = diet; R6Level = level; R6Time = time; R6MealType = mealType; break;
                        }
                    }
                    System.Diagnostics.Debug.WriteLine($"Loaded {mealsTable.Rows.Count} meals successfully");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading last meals: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }


        private void GoDisplayMeals()
        {
            NavigationService.Instance.NavigateTo(typeof(MealListPage));
        }

        private void GoCreateMeal()
        {
            NavigationService.Instance.NavigateTo(typeof(CreateMealPage));
        }

        private void GoGroceryList()
        {
            NavigationService.Instance.NavigateTo(typeof(GroceryListPage));
        }

        private void GoAddBreakfast()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Breakfast");
        }

        private void GoAddLunch()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Lunch");
        }

        private void GoAddDinner()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Dinner");
        }

        private void GoAddSnack()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Snack");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Load the initial water intake from the database
        private string _waterIntake;
        public string WaterIntake
        {
            get => _waterIntake;
            set
            {
                if (_waterIntake != value)
                {
                    _waterIntake = value;
                    OnPropertyChanged(nameof(WaterIntake));
                }
            }
        }

        public void UpdateWaterIntake(float amount)
        {
            if (!float.TryParse(WaterIntake, out float currentIntake))
            {
                currentIntake = 0; // Default to zero if parsing fails
            }

            int number_userId = int.Parse(_userId);
            float newIntake = currentIntake + amount;
            _waterService.UpdateWaterIntake(number_userId, newIntake);
            WaterIntake = newIntake.ToString();
        }

        public void RemoveWaterIntake(float amount)
        {
            if (!float.TryParse(WaterIntake, out float currentIntake))
            {
                currentIntake = 0; // Default to zero if parsing fails
            }

            int number_userId = int.Parse(_userId);
            float newIntake = Math.Max(0, currentIntake - amount); // Ensure we don't go below 0
            _waterService.UpdateWaterIntake(number_userId, newIntake);
            WaterIntake = newIntake.ToString();
        }

    }
}
