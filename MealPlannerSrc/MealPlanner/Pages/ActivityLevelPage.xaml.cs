using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace MealPlanner.Pages
{
    public sealed partial class ActivityLevelPage : Page
    {
        public ActivityLevelPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("ActivityLevelPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in ActivityLevelPage constructor: {ex.Message}");
                throw; // Re-throw the exception to be caught by the global handler
            }
        }
    }
}
