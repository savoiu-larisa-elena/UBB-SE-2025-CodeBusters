using Microsoft.UI.Xaml;
using MealPlanner.Pages;
using MealPlanner.Services;
using System.Diagnostics;
using System;

namespace MealPlanner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("MainWindow initialized successfully.");
                NavigationService.Instance.Initialize(MainFrame); // Initialize NavigationService with MainFrame
                Debug.WriteLine("NavigationService initialized with MainFrame.");
                MainFrame.Navigate(typeof(GoalPage));  // !!! AICI O SA FIE MAIN PAGE !!!
                Debug.WriteLine("Navigated to GoalPage.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in MainWindow constructor: {ex.Message}");
                throw; // Re-throw the exception to be caught by the global handler
            }
        }
    }
}
