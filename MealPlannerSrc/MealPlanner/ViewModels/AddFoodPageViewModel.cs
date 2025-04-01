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
using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using Windows.UI;




namespace MealPlanner.ViewModels
{
    public class AddFoodPageViewModel : ViewModelBase
    {
        private string _searchText;
        private object _selectedItem;
        private bool _isSearchVisible;
        private readonly string connectionString = 
            @"Server=RALUUU_LPT\SQLEXPRESS;Database=MealPlanner;Integrated Security=True;TrustServerCertificate=True";


        public ObservableCollection<object> SearchResults { get; set; } = new ObservableCollection<object>();


        // FOR BACK COMMAND <-
        public AddFoodPageViewModel() 
        {
            BackCommand = new RelayCommand(GoBack);
            NextCommand = new RelayCommand(GoNext);
            AddToMealCommand = new RelayCommand(AddToMeal, CanAddToMeal);


        }

        public ICommand NextCommand { get; }
        public ICommand BackCommand { get; }

        private void GoBack()
        {
            NavigationService.Instance.GoBack();
        }

        private void GoNext()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }


        // for SEARCH BAR INGREDIENT / MEAL

        public ICommand AddToMealCommand { get; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                PerformSearch();
            }
        }

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public bool IsSearchVisible
        {
            get => _isSearchVisible;
            set
            {
                _isSearchVisible = value;
                OnPropertyChanged();
            }
        }


        private void PerformSearch()
        {
            SearchResults.Clear();
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                IsSearchVisible = false;
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Search Meals first
                string mealQuery = "SELECT m_id, m_name FROM meals WHERE m_name LIKE @search + '%'";
                using (SqlCommand cmd = new SqlCommand(mealQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@search", SearchText);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SearchResults.Add(new { Id = reader["m_id"], Name = reader["m_name"], Type = "Meal" });
                        }
                    }
                }

                // If no meals found, search Ingredients
                if (SearchResults.Count == 0)
                {
                    string ingredientQuery = "SELECT i_id, i_name FROM ingredients WHERE i_name LIKE @search + '%'";
                    using (SqlCommand cmd = new SqlCommand(ingredientQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@search", SearchText);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SearchResults.Add(new { Id = reader["i_id"], Name = reader["i_name"], Type = "Ingredient" });
                            }
                        }
                    }
                }
            }

            IsSearchVisible = SearchResults.Count > 0;
        }

        private bool CanAddToMeal() => SelectedItem != null;


        private void AddToMeal()
        {
            if (SelectedItem == null) return;

            int selectedId = (int)SelectedItem.GetType().GetProperty("Id")?.GetValue(SelectedItem);
            string type = (string)SelectedItem.GetType().GetProperty("Type")?.GetValue(SelectedItem);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery;

                if (type == "Meal")
                {
                    insertQuery = "INSERT INTO meal_ingredients (meal_id, ingredient_id, quantity) VALUES (@mealId, NULL, 1)";
                }
                else // Ingredient
                {
                    insertQuery = "INSERT INTO meal_ingredients (meal_id, ingredient_id, quantity) VALUES (NULL, @ingredientId, 1)";
                }

                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    if (type == "Meal")
                    {
                        cmd.Parameters.AddWithValue("@mealId", selectedId);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ingredientId", selectedId);
                    }

                    cmd.ExecuteNonQuery();
                }
            }

            // Reset selection
            SelectedItem = null;
            SearchText = "";
            IsSearchVisible = false;
        }


        // FOR SELECTED MEAL TYPE


        private SolidColorBrush _breakfastColor = new SolidColorBrush(Colors.Transparent);
        private SolidColorBrush _lunchColor = new SolidColorBrush(Colors.Transparent);
        private SolidColorBrush _dinnerColor = new SolidColorBrush(Colors.Transparent);
        private SolidColorBrush _snackColor = new SolidColorBrush(Colors.Transparent);
        public SolidColorBrush BreakfastColor
        {
            get => _breakfastColor;
            set { _breakfastColor = value; OnPropertyChanged(); }
        }
        public SolidColorBrush LunchColor
        {
            get => _lunchColor;
            set { _lunchColor = value; OnPropertyChanged(); }
        }
        public SolidColorBrush DinnerColor
        {
            get => _dinnerColor;
            set { _dinnerColor = value; OnPropertyChanged(); }
        }
        public SolidColorBrush SnackColor
        {
            get => _snackColor;
            set { _snackColor = value; OnPropertyChanged(); }
        }

        public void SetCategoryHighlight(string category)
        {
            // Default colors
            BreakfastColor = new SolidColorBrush(Color.FromArgb(255, 199, 110, 78)); // Reddish brown
            LunchColor = new SolidColorBrush(Color.FromArgb(255, 91, 119, 105)); // Greenish grey
            DinnerColor = new SolidColorBrush(Color.FromArgb(255, 181, 136, 94)); // Light brown
            SnackColor = new SolidColorBrush(Color.FromArgb(255, 19, 50, 36)); // Dark green

            // Highlight selected category
            SolidColorBrush highlightColor = new SolidColorBrush(Color.FromArgb(255, 227, 199, 177)); // Beige



            switch (category)
            {
                case "Breakfast": BreakfastColor = highlightColor; break;
                case "Lunch": LunchColor = highlightColor; break;
                case "Dinner": DinnerColor = highlightColor; break;
                case "Snack": SnackColor = highlightColor; break;
            }
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
