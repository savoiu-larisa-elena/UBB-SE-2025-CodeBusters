using MealPlanner.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace MealPlanner.Pages
{
    public sealed partial class MealListPage : Page
    {
        public MealListPage()
        {
            this.InitializeComponent();
            this.DataContext = new MealListPageViewModel();

        }
    }
}