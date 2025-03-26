using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace MealPlanner.Pages
{
    public sealed partial class GoalPage : Page
    {
        public GoalPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("GoalPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in GoalPage constructor: {ex.Message}");
                throw; // Re-throw the exception to be caught by the global handler
            }
        }
    }
}
