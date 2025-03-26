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
using System.Net.Http;
using MealPlanner.ViewModels;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MealPlanner.Pages
{
    public sealed partial class MealDisplayPage : Page
    {
        public MealViewModel ViewModel { get; set; }
        public ObservableCollection<string> Directions { get; set; }
        public ObservableCollection<string> Ingredients { get; set; }

        public MealDisplayPage()
        {
            this.InitializeComponent();
            ViewModel = new MealViewModel();
            DataContext = ViewModel;
            Directions = new ObservableCollection<string>
            {
                "1. Prep: Slice chicken, bell peppers, and carrots.",
                "2. Cook Chicken: Heat oil in a pan, cook until golden.",
                "3. Add Veggies: Stir in peppers, carrots, and garlic.",
                "4. Sauce: Mix soy sauce, honey, and cornstarch.",
                "5. Finish: Stir everything, serve with rice."
            };

            Ingredients = new ObservableCollection<string>
            {
                "1 chicken breast (sliced)",
                "1 bell pepper (sliced)",
                "1 carrot (sliced)",
                "2 cloves garlic (minced)",
                "2 tbsp soy sauce",
                "1 tbsp honey",
                "1 tsp cornstarch",
                "1 tbsp oil",
                "Cooked rice"
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is MealViewModel meal)
            {
                ViewModel = meal;
                DataContext = ViewModel;
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void NavigateToGroceryList(object sender, RoutedEventArgs e)
        {
            if (Frame != null)
            {
                Frame.Navigate(typeof(GroceryListPage));
            }
        }

    }
}
