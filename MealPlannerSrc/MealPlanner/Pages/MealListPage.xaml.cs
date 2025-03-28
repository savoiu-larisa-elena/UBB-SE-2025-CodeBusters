using MealPlanner.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace MealPlanner.Pages
{
    public sealed partial class MealListPage : Page
    {
        public MealListPage()
        {
            this.InitializeComponent();
            this.DataContext = App.MealListViewModel;
            this.Loaded += MealListPage_Loaded;
        }

        private async void MealListPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await App.MealListViewModel.LoadMealsAsync();
                Debug.WriteLine("Meals loaded successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading meals: {ex.Message}");
            }
        }
    }
}