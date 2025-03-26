using MealPlanner.Pages;
using MealPlanner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MealPlanner.ViewModels
{
    class MainViewModel
    {
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

    }
}
