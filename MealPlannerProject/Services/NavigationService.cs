using MealPlannerProject.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;
        private Frame _mainFrame;

        public static NavigationService Instance => _instance ??= new NavigationService();

        public void Initialize(Frame mainFrame)
        {
            _mainFrame = mainFrame;
        }

        public void NavigateTo(Type pageType, object parameter = null)
        {
            _mainFrame?.Navigate(pageType, parameter);
        }

        public void GoBack()
        {
            if (_mainFrame?.CanGoBack == true)
            {
                _mainFrame.GoBack();
            }
        }

        internal void NavigateTo(Type type, BodyMetricsViewModel bodyMetricsViewModel)
        {
            _mainFrame?.Navigate(type, bodyMetricsViewModel);
        }

        internal void NavigateTo(Type type, GoalPageViewModel goalPageViewModel)
        {
            _mainFrame?.Navigate(type, goalPageViewModel);
        }

        internal void NavigateTo(Type type, ActivityLevelViewModel activityLevelViewModel)
        {
            _mainFrame?.Navigate(type, activityLevelViewModel);
        }

        internal void NavigateTo(Type type, CookingLevelViewModel cookingLevelViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
