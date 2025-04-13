using MealPlannerProject.Models;
using MealPlannerProject.Services;
using MealPlannerProject.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

    public sealed partial class MealListPage : Page
    {
        public MealListPage()
        {
            this.InitializeComponent();
            this.DataContext = new MealListViewModel(new MealService());
        }
    }
}