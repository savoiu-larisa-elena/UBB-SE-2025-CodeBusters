using System.Collections.ObjectModel;
using System.Windows.Input;
using MealPlannerProject.Services;
using MealPlannerProject.Pages;

namespace MealPlannerProject.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        public ICommand GetStartedCommand { get; }
        public ObservableCollection<string> Items { get; }

        public WelcomeViewModel()
        {
            GetStartedCommand = new RelayCommand(OnGetStarted);
            Items = new ObservableCollection<string>();
        }

        private void OnGetStarted()
        {
            NavigationService.Instance.NavigateTo(typeof(UserPage));
        }
    }
}
