using System.Collections.ObjectModel;
using System.Windows.Input;
using MealPlanner.Services;
using MealPlanner.Pages;

namespace MealPlanner.ViewModels
{
    public class GoalViewModel : ViewModelBase
    {
        public ObservableCollection<string> Goals { get; } = new ObservableCollection<string>
        {
            "Lose weight",
            "Gain weight",
            "Maintain weight",
            "Body recomposition",
            "Improve overall health"
        };

        public ICommand BackCommand { get; }
        public ICommand NextCommand { get; }

        public GoalViewModel()
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
            NavigationService.Instance.NavigateTo(typeof(ActivityLevelPage));
        }
    }
}
