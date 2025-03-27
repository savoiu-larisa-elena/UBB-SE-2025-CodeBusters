using MealPlanner.Pages;
using MealPlanner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using System.Collections.ObjectModel;
using MealPlanner.Pages;

namespace MealPlanner.ViewModels
{
    public class BodyMetricsViewModel : ViewModelBase
    {
        
        public ICommand NextCommand { get; }

        public BodyMetricsViewModel()
        {
            NextCommand = new RelayCommand(GoNext);
        }


        private void GoNext()
        {
            NavigationService.Instance.NavigateTo(typeof(GoalPage));
        }


    }
}
