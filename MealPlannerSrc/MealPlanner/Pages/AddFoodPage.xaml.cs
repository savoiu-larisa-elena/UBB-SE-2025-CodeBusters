using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Diagnostics;
using MealPlanner.ViewModels;


namespace MealPlanner.Pages
{

    public sealed partial class AddFoodPage : Page
    {
        public AddFoodPageViewModel AddFoodPageViewModel { get; } = new AddFoodPageViewModel(); // Initialize ViewModel

        public AddFoodPage()
        {
            try
            {
                this.InitializeComponent();
                this.DataContext = AddFoodPageViewModel;
                Debug.WriteLine("AddFoodPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in AddFoodPage constructor: {ex.Message}");
                throw; // Re-throw the exception to be caught by the global handler
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is string selectedMeal)
            {
                AddFoodPageViewModel.SetCategoryHighlight(selectedMeal);
            }
        }

    }
}
