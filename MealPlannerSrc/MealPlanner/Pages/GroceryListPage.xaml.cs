using MealPlanner.Models;
using MealPlanner.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MealPlanner.Pages
{
    public sealed partial class GroceryListPage : Page
    {
        public GroceryViewModel ViewModel { get; } = new();

        public GroceryListPage() => this.InitializeComponent();

        private void AddGroceryIngredient_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.DataContext is GroceryIngredient selectedIngredient)
            {
                ViewModel.AddGroceryIngredient(selectedIngredient);
            }
        }

        private void NavigateToMealDisplay(object sender, RoutedEventArgs e)
        {
            if (Frame != null)
            {
                Frame.Navigate(typeof(MealDisplayPage));
            }
        }

    }
}

