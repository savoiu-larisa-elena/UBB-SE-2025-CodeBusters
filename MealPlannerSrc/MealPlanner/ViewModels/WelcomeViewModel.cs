using System.Collections.ObjectModel;
using System.Windows.Input;
using MealPlanner.Services;
using MealPlanner.Pages;

namespace MealPlanner.ViewModels
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
            NavigationService.Instance.NavigateTo(typeof(BodyMetricsPage));
        }
    }
}
