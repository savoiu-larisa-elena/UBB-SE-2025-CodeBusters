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
using System.Diagnostics;
using MealPlannerProject.ViewModels;


namespace MealPlannerProject.Pages
{

    public sealed partial class YoureAllSetPage : Page
    {
        private YoureAllSetViewModel viewModel = new YoureAllSetViewModel();

        public YoureAllSetPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("YoureAllSetPage initialized successfully.");
                this.DataContext = viewModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in YoureAllSetPage constructor: {ex.Message}");
                throw; 
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is DietaryPreferencesViewModel dpViewModel)
            {
                Debug.WriteLine($"GoalPage received user: {dpViewModel.FirstName} {dpViewModel.LastName}");
                viewModel.SetUserInfo(dpViewModel.FirstName, dpViewModel.LastName);
            }
        }
    }
}
