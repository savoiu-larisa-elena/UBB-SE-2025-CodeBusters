namespace MealPlannerProject.Pages
{
    using System;
    using System.Diagnostics;
    using MealPlannerProject.ViewModels;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;

    public sealed partial class ActivityLevelPage : Page
    {
        private readonly ActivityLevelViewModel viewModel = new ();

        public ActivityLevelPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("ActivityLevelPage initialized successfully.");
                this.DataContext = this.viewModel;
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
                this.viewModel.SetUserInfo(goalPageViewModel.FirstName, goalPageViewModel.LastName);
            }
        }

        private static void ShowErrorDialog(string v)
        {
            _ = new ContentDialog
            {
                Title = "Error",
                Content = v,
                CloseButtonText = "OK",
            };
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.viewModel.SelectedActivity))
                {
                    ShowErrorDialog("Please select an activity level.");
                    return;
                }

                this.Frame.Navigate(typeof(CookingLevelPage), this.viewModel);
            }
            catch (Exception ex)
            {
                ShowErrorDialog($"An error occurred: {ex.Message}");
            }
        }
    }
}
