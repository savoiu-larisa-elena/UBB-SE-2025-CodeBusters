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
using Windows.System;
using Microsoft.UI.Xaml.Controls;
using MealPlanner.Models;




namespace MealPlanner.ViewModels
{
    public class AddFoodPageViewModel : ViewModelBase
    {
        private string _searchText;
        private object _selectedItem;
        private bool _isSearchVisible;
        private string _selectedUnit = "g";



        private readonly string connectionString = 
            @"Server=RALUUU_LPT\SQLEXPRESS;Database=MealPlanner;Integrated Security=True;TrustServerCertificate=True";


        public ObservableCollection<object> SearchResults { get; set; } = new ObservableCollection<object>();


        // FOR BACK COMMAND <-
        public AddFoodPageViewModel() 
        {
            BackCommand = new RelayCommand(GoBack);
            NextCommand = new RelayCommand(GoNext);
            AddToMealCommand = new RelayCommand(AddToMeal, CanAddToMeal);
            FetchServingUnits();



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
            if (SelectedItem == null)
            {
                Console.WriteLine("SelectedItem is null.");
                return;
            }

            int selectedId = (int)SelectedItem.GetType().GetProperty("Id")?.GetValue(SelectedItem);
            string type = (string)SelectedItem.GetType().GetProperty("Type")?.GetValue(SelectedItem);

            Console.WriteLine($"Selected Item ID: {selectedId}");
            Console.WriteLine($"Selected Item Type: {type}");
            Console.WriteLine($"Servings Count: {servingsCount}");
            Console.WriteLine($"Unit Name: {SelectedUnit}");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Database connection established.");

                    string insertQuery = @"
                                        INSERT INTO daily_meals (m_id, date_eaten, servings, unit_name, total_calories, 
                                    total_protein, total_carbohydrates, total_fat, total_fiber, total_sugar)
                                        SELECT 
                                            @mealId,  -- <-- Update parameter here
                                            GETDATE(),  
                                            @servings, 
                                            @unit_name, 
                                            m.calories * @servings AS total_calories, 
                                            m.protein * @servings AS total_protein, 
                                            m.carbohydrates * @servings AS total_carbohydrates, 
                                            m.fat * @servings AS total_fat, 
                                            m.fiber * @servings AS total_fiber, 
                                            m.sugar * @servings AS total_sugar
                                        FROM meals m
                                        WHERE m.m_id = @mealId";  // <-- Update parameter here as well


                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@mealId", selectedId);
                        cmd.Parameters.AddWithValue("@unit_name", SelectedUnit);
                        cmd.Parameters.AddWithValue("@servings", servingsCount);

                        int result = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Rows affected: {result}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting data: " + ex.Message);
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












        // SERVING UNITS

        public ObservableCollection<ServingUnitModel> ServingUnits { get; set; } = new ObservableCollection<ServingUnitModel>();

        private void FetchServingUnits()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT unit_name FROM serving_units";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var servingUnits = new ObservableCollection<ServingUnitModel>();
                        while (reader.Read())
                        {
                            servingUnits.Add(new ServingUnitModel
                            {
                                UnitName = reader["unit_name"].ToString()
                            });
                        }
                        ServingUnits = servingUnits;
                    }
                }
            }
        }
        public string SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                if (_selectedUnit != value)
                {
                    _selectedUnit = value;
                    OnPropertyChanged(nameof(SelectedUnit));
                }
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
