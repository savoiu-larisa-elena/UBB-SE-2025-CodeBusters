using System.Collections.ObjectModel;
using System.Windows.Input;
using MealPlanner.Services;
using MealPlanner.Pages;

namespace MealPlanner.ViewModels
{
    public class ActivityLevelViewModel : ViewModelBase
    {
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

        private void GoNext()
        {
            NavigationService.Instance.NavigateTo(typeof(CookingLevelPage));
        }
    }
}
