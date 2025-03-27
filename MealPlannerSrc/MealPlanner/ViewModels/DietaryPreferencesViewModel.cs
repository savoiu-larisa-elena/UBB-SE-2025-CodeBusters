using MealPlanner.Pages;
using MealPlanner.Services;
using System;
using System.Windows.Input;

namespace MealPlanner.ViewModels
{
    public class DietaryPreferencesViewModel
    {
        public ICommand BackCommand { get; set; }
        public ICommand NextCommand { get; set; }

        public string OtherDiet { get; set; } = string.Empty;
        public string Allergens { get; set; } = string.Empty;

        public DietaryPreferencesViewModel()
        {
            BackCommand = new RelayCommand(BackAction);
            NextCommand = new RelayCommand(NextAction);
        }
        private void BackAction()
        {
            NavigationService.Instance.GoBack();
        }

        private void NextAction()
        {
            NavigationService.Instance.NavigateTo(typeof(YoureAllSetPage));
        }
    }
}
