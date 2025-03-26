using Microsoft.UI.Xaml.Controls;
using System;

namespace MealPlanner.Services
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

        public void NavigateTo(Type pageType)
        {
            _mainFrame?.Navigate(pageType);
        }

        public void GoBack()
        {
            if (_mainFrame?.CanGoBack == true)
            {
                _mainFrame.GoBack();
            }
        }
    }
}
