using MealPlannerProject.Services;
using System.Windows.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using MealPlannerProject.Pages;

namespace MealPlannerProject.ViewModels
{
    public class BodyMetricsViewModel : ViewModelBase
    {
        public ICommand NextCommand { get; }

        private string weight = string.Empty;
        private string height = string.Empty;
        private string targetWeight = string.Empty;
        private string firstName = string.Empty;
        private string lastName = string.Empty;

        public string Weight
        {
            get => weight;
            set
            {
                weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        public string Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        public string TargetWeight
        {
            get => targetWeight;
            set
            {
                targetWeight = value;
                OnPropertyChanged(nameof(TargetWeight));
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private BodyMetricService bodyMetricService = new Services.BodyMetricService();

        public BodyMetricsViewModel()
        {
            NextCommand = new RelayCommand(GoNext);
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private void GoNext()
        {
            try
            {
                bodyMetricService.addBodyMetrics(FirstName, LastName, Weight, Height, TargetWeight);
                NavigationService.Instance.NavigateTo(typeof(GoalPage), this);
            }
            catch (Exception ex)
            {
                // Handle any errors here  
                Debug.WriteLine($"Error in GoNext: {ex.Message}");
            }
        }
    }
}
