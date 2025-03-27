using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MealPlanner.Services;

using System.Collections.ObjectModel;
using MealPlanner.Pages;




namespace MealPlanner.ViewModels
{
    public class AddFoodPageViewModel : ViewModelBase
    {


        // FOR BACK COMMAND <-

        public AddFoodPageViewModel() 
        {
            BackCommand = new RelayCommand(GoBack);
            NextCommand = new RelayCommand(GoNext);

        }

        public ICommand NextCommand { get; }


        public ICommand BackCommand { get; }

        private void GoBack()
        {
            NavigationService.Instance.GoBack();
        }

        private void GoNext()
        {
            NavigationService.Instance.NavigateTo(typeof(GoalPage));
        }

        private int servingsCount = 0;
        public string ServingsCount
        {
            get => $"{servingsCount} servings";
            set
            {
                if (int.TryParse(value, out int newValue))
                {
                    servingsCount = newValue;
                    OnPropertyChanged();
                }
            }
        }



    }
}
