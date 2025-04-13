namespace MealPlannerProject.Pages
{
    using System;
    using System.Diagnostics;
    using MealPlannerProject.ViewModels;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;

    public sealed partial class CookingLevelPage : Page
    {
        private CookingLevelViewModel viewModel = new CookingLevelViewModel();

        public CookingLevelPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("CookingLevelPage initialized successfully.");
                this.DataContext = this.viewModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in CookingLevelPage constructor: {ex.Message}");
                throw; // Re-throw the exception to be caught by the global handler
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is ActivityLevelViewModel activityLevelViewModel)
            {
                Debug.WriteLine($"Cooking page received user: {activityLevelViewModel.FirstName} {activityLevelViewModel.LastName}");
                this.viewModel.SetUserInfo(activityLevelViewModel.FirstName, activityLevelViewModel.LastName);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.viewModel.SelectedCookingSkill))
                {
                    this.ShowErrorDialog("Please select an activity level.");
                    return;
                }

                // this.Frame.Navigate(typeof(CookingLevelPage), viewModel);
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog($"An error occurred: {ex.Message}");
            }
        }

        private void ShowErrorDialog(string v)
        {
        }
    }
}
