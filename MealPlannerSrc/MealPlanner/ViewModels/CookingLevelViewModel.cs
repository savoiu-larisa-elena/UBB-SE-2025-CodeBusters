using System.Collections.ObjectModel;
using System.Windows.Input;
using MealPlanner.Services;

namespace MealPlanner.ViewModels
{
    public partial class CookingLevelViewModel : ViewModelBase
    {
        public ObservableCollection<string> CookingLevels { get; } = new ObservableCollection<string>
        {
            "I'm a beginner",
            "I cook sometimes",
            "I love cooking",
            "I prefer quick meals",
            "I meal prep"
        };

        public ICommand BackCommand { get; }
        public ICommand NextCommand { get; }

        public CookingLevelViewModel()
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
            // Implement navigation to the next page
            // NavigationService.Instance.NavigateTo(typeof(NextPage)); // Replace 'NextPage' with the actual next page
        }
    }
}
