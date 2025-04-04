using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MealPlannerProject.Services;
using MealPlannerProject.Pages;

namespace MealPlannerProject.Pages
{
    public sealed partial class UserPage : Page
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        private Services.UserPageService userPageService = new Services.UserPageService();

        public UserPage()
        {
            this.InitializeComponent();
        }

        public string GetUserFullName()
        {
            return $"{LastName} {FirstName}";
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            FirstName = FirstNameTextBox.Text.Trim();
            LastName = LastNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Please enter both your first and last name.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                dialog.ShowAsync();
                return;
            }
            bool nextPageNavigation = userPageService.userHasAnAccount(LastName + " " + FirstName);
            if(nextPageNavigation)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                userPageService.insertNewUser(LastName + " " + FirstName);
                this.Frame.Navigate(typeof(BodyMetricsPage), this);
            }
        }
    }
}