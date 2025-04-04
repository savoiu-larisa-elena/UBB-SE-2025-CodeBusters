using MealPlannerProject.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Diagnostics;

namespace MealPlannerProject.Pages
{
    public sealed partial class ActivityLevelPage : Page
    {

        private ActivityLevelViewModel viewModel = new ActivityLevelViewModel();    

        public ActivityLevelPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("ActivityLevelPage initialized successfully.");
                this.DataContext = viewModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in ActivityLevelPage constructor: {ex.Message}");
                throw; // Re-throw the exception to be caught by the global handler
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is GoalPageViewModel goalPageViewModel)
            {
                Debug.WriteLine($"GoalPage received user: {goalPageViewModel.FirstName} {goalPageViewModel.LastName}");
                viewModel.SetUserInfo(goalPageViewModel.FirstName, goalPageViewModel.LastName);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(viewModel.SelectedActivity))
                {
                    ShowErrorDialog("Please select an activity level.");
                    return;
                }
                this.Frame.Navigate(typeof(CookingLevelPage), viewModel);
            }
            catch (Exception ex)
            {
                ShowErrorDialog($"An error occurred: {ex.Message}");
            }
        }

        private void ShowErrorDialog(string v)
        {
         
        }
    }
}
