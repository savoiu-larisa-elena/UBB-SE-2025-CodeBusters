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
using MealPlanner.ViewModels;

namespace MealPlanner.Pages
{
    public sealed partial class DietaryPreferencesPage : Page
    {
        

        public DietaryPreferencesPage()
        {
            this.InitializeComponent();
            this.DataContext = new DietaryPreferencesViewModel();
        }

        private void OnBackCommandExecuted()
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnNextCommandExecuted()
        {
            if (Frame != null)
            {
                Frame.Navigate(typeof(YoureAllSetPage));
            }
        }
    }
}
