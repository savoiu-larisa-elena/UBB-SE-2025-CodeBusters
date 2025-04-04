using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Diagnostics;
using MealPlannerProject.ViewModels;
using MealPlannerProject.Services;

namespace MealPlannerProject.Pages
{
    public sealed partial class GoalPage : Page
    {
        private GoalPageViewModel viewModel;

        public GoalPage()
        {
            try
            {
                this.InitializeComponent();
                viewModel = new GoalPageViewModel();
                this.DataContext = viewModel;
                Debug.WriteLine("GoalPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in GoalPage constructor: {ex.Message}");
                throw;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is BodyMetricsViewModel metricsViewModel)
            {
                Debug.WriteLine($"GoalPage received user: {metricsViewModel.FirstName} {metricsViewModel.LastName}");
                viewModel.SetUserInfo(metricsViewModel.FirstName, metricsViewModel.LastName);
            }
        }


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(viewModel.SelectedGoal))
                {
                    ShowErrorDialog("Please select a goal.");
                    return;
                }
                
                this.Frame.Navigate(typeof(ActivityLevelPage), viewModel);
            }
            catch (Exception ex)
            {
                ShowErrorDialog($"An error occurred: {ex.Message}");
            }
        }

        private async void ShowErrorDialog(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Error",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await dialog.ShowAsync();
        }
    }
}
