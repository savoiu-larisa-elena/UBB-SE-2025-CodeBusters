using MealPlanner.Pages;
using MealPlanner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MealPlanner.ViewModels
{
    public class YoureAllSetViewModel
    {
        public ICommand NextCommand { get; }

        public YoureAllSetViewModel()
        {
            NextCommand = new RelayCommand(GoNext);
        }

        private void GoNext()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }
    }
}
