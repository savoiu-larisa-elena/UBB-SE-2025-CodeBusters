using System.Collections.ObjectModel;
using System.Windows.Input;
using MealPlannerProject.Services;
using MealPlannerProject.Pages;
using MealPlannerProject.ViewModels;
using System.ComponentModel;
using MealPlannerProject.Interfaces.Services;

namespace MealPlannerProject.ViewModels
{
    public class ActivityLevelViewModel : ViewModelBase
    {

        private string _selectedActivity;

        private IActivityPageService activityPageService = new ActivityPageService();
        public ObservableCollection<string> ActivityLevels { get; } = new ObservableCollection<string>
        {
            "Sedentary",
            "Lightly Active",
            "Moderately Active",
            "Very Active",
            "Super Active"
        };

        public ICommand BackCommand { get; }
        public ICommand NextCommand { get; }

        public ActivityLevelViewModel()
        {
            BackCommand = new RelayCommand(GoBack);
            NextCommand = new RelayCommand(GoNext);
        }

        private void GoBack()
        {
            NavigationService.Instance.GoBack();
        }

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

        public string SelectedActivity
        {
            get => _selectedActivity;
            set
            {
                _selectedActivity = value;
                OnPropertyChanged(nameof(SelectedActivity));
            }
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }


        private void GoNext()
        {
            activityPageService.AddActivity(FirstName, LastName, SelectedActivity);
            NavigationService.Instance.NavigateTo(typeof(CookingLevelPage), this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
 