using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MealPlannerProject.Pages;
using MealPlannerProject.Services;
using Windows.Networking;

namespace MealPlannerProject.ViewModels
{
    class GoalPageViewModel
    {
        public ObservableCollection<string> Goals { get; } = new ObservableCollection<string>
        {
            "Lose weight",
            "Gain weight",
            "Maintain weight",
            "Body recomposition",
            "Improve overall health"
        };

        private string _selectedGoal;
        public string SelectedGoal
        {
            get => _selectedGoal;
            set
            {
                _selectedGoal = value;
                OnPropertyChanged(nameof(SelectedGoal));
            }
        }

        public ICommand SelectGoalCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand NextCommand { get; }

        public GoalPageViewModel()
        {
            BackCommand = new RelayCommand(GoBack);
            NextCommand = new RelayCommand(GoNext);
            SelectGoalCommand = new RelayCommand<string>(OnGoalSelected);
        }

        private void OnGoalSelected(string goal)
        {
            SelectedGoal = goal;
        }


        private void GoBack()
        {
            NavigationService.Instance.GoBack();
        }

        private GoalPageService goalPageService = new GoalPageService();

        private string firstName;
        private string lastName;

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
