namespace MealPlannerProject.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.Input;
    using MealPlannerProject.Pages;
    using MealPlannerProject.Services;
    using Windows.Networking;

    internal class GoalPageViewModel
    {
        public ObservableCollection<string> Goals { get; } = new ObservableCollection<string>
            {
                "Lose weight",
                "Gain weight",
                "Maintain weight",
                "Body recomposition",
                "Improve overall health",
            };

        private string selectedGoal = string.Empty; // Initialize to avoid CS8618

        public string SelectedGoal
        {
            get => this.selectedGoal;
            set
            {
                this.selectedGoal = value;
                this.OnPropertyChanged(nameof(this.SelectedGoal));
            }
        }

        public ICommand SelectGoalCommand { get; }

        public ICommand BackCommand { get; }

        public ICommand NextCommand { get; }

        public GoalPageViewModel()
        {
            this.BackCommand = new RelayCommand(this.GoBack);
            this.NextCommand = new RelayCommand(this.GoNext);
            this.SelectGoalCommand = new RelayCommand<string>(this.OnGoalSelected);
        }

        private void OnGoalSelected(string? goal)
        {
            this.SelectedGoal = (goal != null) ? goal : string.Empty;
        }

        private void GoBack()
        {
            NavigationService.Instance.GoBack();
        }

        private GoalPageService goalPageService = new GoalPageService();

        private string firstName = string.Empty;
        private string lastName = string.Empty;

        public string FirstName
        {
            get => this.firstName;
            set
            {
                this.firstName = value;
                this.OnPropertyChanged(nameof(this.FirstName));
            }
        }

        public string LastName
        {
            get => this.lastName;
            set
            {
                this.lastName = value;
                this.OnPropertyChanged(nameof(this.LastName));
            }
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private void GoNext()
        {
            goalPageService.addGoals(FirstName, LastName, SelectedGoal);
            NavigationService.Instance.NavigateTo(typeof(ActivityLevelPage), this);
        }

        public event PropertyChangedEventHandler? PropertyChanged; // Mark as nullable to avoid CS8618
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
