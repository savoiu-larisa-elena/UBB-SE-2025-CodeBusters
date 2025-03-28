using MealPlanner.Pages;
using MealPlanner.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MealPlanner.ViewModels
{
    class MainViewModel : INotifyPropertyChanged 
    {
        // Generic method to update properties
        private string _circleText;
        private string _baseGoalCal;
        private string _foodCal;
        private string _exerciseCal;
        private string _breakfastCal;
        private string _lunchCal;
        private string _dinnerCal;
        private string _snackCal;
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

        //
        public string BreakfastCal { get => _breakfastCal; set => SetProperty(ref _breakfastCal, value); }
        public string LunchCal { get => _lunchCal; set => SetProperty(ref _lunchCal, value); }
        public string DinnerCal { get => _dinnerCal; set => SetProperty(ref _dinnerCal, value); }
        public string SnackCal { get => _snackCal; set => SetProperty(ref _snackCal, value); }
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

        // Commands - habar n am ces imi merge si fara
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
        public ICommand SmallCup { get; }
        public ICommand BigCup { get; }
        public ICommand SmallBottle { get; }
        public ICommand BigBottle { get; }
        public ICommand AddBreakfast { get; }
        public ICommand AddLunch { get; }
        public ICommand AddDinner { get; }
        public ICommand AddSnack { get; }

        public MainViewModel()
        {

            // Initialize values - cred ca aici bagi baza de date
            CircleText = "789";
            BaseGoal_Cal = "2000";
            Food_Cal = "1500";
            Exercise_Cal = "500";
            TotalPr = "78"; GoalPr = "98"; LeftPr = "20";
            TotalCarb = "150"; GoalCarb = "200"; LeftCarb = "50";
            TotalFib = "25"; GoalFib = "30"; LeftFib = "5";
            TotalFat = "70"; GoalFat = "90"; LeftFat = "20";
            TotalSug = "30"; GoalSug = "50"; LeftSug = "20";
            BreakfastCal = "20"; LunchCal = "20"; DinnerCal = "20"; SnackCal = "20";
            WaterPercent = "50"; // nu e percent dar o sa l calculez eu, tu pune aici valoarea din ml
            WaterGoal = "2000";

            // Initialize values
            for (int i = 0; i < _rValues.Length; i++)
            {
                _rValues[i] = $"Default {i}";
            }

            // Command setup - habar n am pt ce s astea imi merge si fara
            RefreshCircleTextCommand = new RelayCommand(() => CircleText = "Updated 789");
            RefreshBaseGoalTextCommand = new RelayCommand(() => BaseGoal_Cal = "Updated 2000");
            RefreshFoodTextCommand = new RelayCommand(() => Food_Cal = "Updated 1500");
            RefreshExerciseTextCommand = new RelayCommand(() => Exercise_Cal = "Updated 500");

            RefreshTotalPrTextCommand = new RelayCommand(() => TotalPr = "Updated 98");
            RefreshGoalPrTextCommand = new RelayCommand(() => GoalPr = "Updated 78");
            RefreshLeftPrTextCommand = new RelayCommand(() => LeftPr = "Updated 20");

            RefreshTotalCarbTextCommand = new RelayCommand(() => TotalCarb = "Updated 150");
            RefreshGoalCarbTextCommand = new RelayCommand(() => GoalCarb = "Updated 200");
            RefreshLeftCarbTextCommand = new RelayCommand(() => LeftCarb = "Updated 50");

            RefreshTotalFibTextCommand = new RelayCommand(() => TotalFib = "Updated 25");
            RefreshGoalFibTextCommand = new RelayCommand(() => GoalFib = "Updated 30");
            RefreshLeftFibTextCommand = new RelayCommand(() => LeftFib = "Updated 5");

            RefreshTotalFatTextCommand = new RelayCommand(() => TotalFat = "Updated 70");
            RefreshGoalFatTextCommand = new RelayCommand(() => GoalFat = "Updated 90");
            RefreshLeftFatTextCommand = new RelayCommand(() => LeftFat = "Updated 20");

            RefreshTotalSugTextCommand = new RelayCommand(() => TotalSug = "Updated 30");
            RefreshGoalSugTextCommand = new RelayCommand(() => GoalSug = "Updated 50");
            RefreshLeftSugTextCommand = new RelayCommand(() => LeftSug = "Updated 20");


            DisplayMeals = new RelayCommand(GoDisplayMeals);

            CreateMeal = new RelayCommand(GoCreateMeal);

            GroceryList = new RelayCommand(GoGroceryList);

            SmallCup = new RelayCommand(GoSmallCup);

            BigCup = new RelayCommand(GoBigCup);

            SmallBottle = new RelayCommand(GoSmallBottle);

            BigBottle = new RelayCommand(GoBigBottle);

            AddBreakfast = new RelayCommand(GoAddBreakfast);

            AddLunch = new RelayCommand(GoAddLunch);

            AddDinner = new RelayCommand(GoAddDinner);

            AddSnack = new RelayCommand(GoAddSnack);


        }

        private void GoDisplayMeals()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }

        private void GoCreateMeal()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }

        private void GoGroceryList()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }

        private void GoSmallCup()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }

        private void GoBigCup()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }

        private void GoSmallBottle()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }

        private void GoBigBottle() 
        { 
            NavigationService.Instance.NavigateTo(typeof(MainPage)); 
        }

        private void GoAddBreakfast()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }

        private void GoAddLunch()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }

        private void GoAddDinner()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }

        private void GoAddSnack()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    }
}
