using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace MealPlanner.Pages
{
    public sealed partial class CookingLevelPage : Page
    {
        public CookingLevelPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("CookingLevelPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in CookingLevelPage constructor: {ex.Message}");
                throw; // Re-throw the exception to be caught by the global handler
            }
        }
    }
}
