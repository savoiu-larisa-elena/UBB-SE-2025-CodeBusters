using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.UI.Xaml.Controls;
using MealPlannerProject.Services;
using System;
using System.Threading.Tasks;
using MealPlannerProject.Models;
using CommunityToolkit.Mvvm.Input;
using Windows.Storage;
using System.IO;
using System.Linq;

namespace MealPlannerProject.ViewModels
{
    public class CreateMealViewModel : ViewModelBase
    {
        private readonly MealService _mealService;
        private string _mealName;
        private string _cookingTime;
        private string _selectedMealType;
        private string _selectedCookingLevel;
        private ObservableCollection<string> _directions;
        private ObservableCollection<MealIngredient> _ingredients;
        private StorageFile _selectedImage;

        // Calculated macros
        private int _calories;
        private int _protein;
        private int _carbs;
        private int _fats;
        private int _fiber;
        private int _sugar;

        public CreateMealViewModel()
        {
            _mealService = new MealService();

            // Initialize collections
            _directions = new ObservableCollection<string>();
            _ingredients = new ObservableCollection<MealIngredient>();

            // Initialize commands
            GoBackCommand = new RelayCommand(OnGoBack);
            AddDirectionCommand = new RelayCommand(OnAddDirection);
            AddIngredientCommand = new RelayCommand(OnAddIngredient);
            SelectMealTypeCommand = new RelayCommand<string?>(OnSelectMealType);
            SelectCookingLevelCommand = new RelayCommand<string?>(OnSelectCookingLevel);
        }

        public string MealName
        {
            get => _mealName;
            set
            {
                if (_mealName != value)
                {
                    _mealName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CookingTime
        {
            get => _cookingTime;
            set
            {
                if (_cookingTime != value)
                {
                    _cookingTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedMealType
        {
            get => _selectedMealType;
            set
            {
                if (_selectedMealType != value)
                {
                    _selectedMealType = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedCookingLevel
        {
            get => _selectedCookingLevel;
            set
            {
                if (_selectedCookingLevel != value)
                {
                    _selectedCookingLevel = value;
                    OnPropertyChanged();
                }
            }
        }

        public StorageFile SelectedImage
        {
            get => _selectedImage;
            set
            {
                if (_selectedImage != value)
                {
                    _selectedImage = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Directions
        {
            get => _directions;
            set
            {
                if (_directions != value)
                {
                    _directions = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> IngredientNames
        {
            get => new(_ingredients.Select(i => $"{i.IngredientName}: {i.Quantity} servings"));
        }

        public ObservableCollection<MealIngredient> Ingredients
        {
            get => _ingredients;
            set
            {
                if (_ingredients != value)
                {
                    _ingredients = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IngredientNames));
                    CalculateTotalMacros();
                }
            }
        }

        public ICommand GoBackCommand { get; }
        public ICommand AddDirectionCommand { get; }
        public ICommand AddIngredientCommand { get; }
        public ICommand SelectMealTypeCommand { get; }
        public ICommand SelectCookingLevelCommand { get; }

        private void CalculateTotalMacros()
        {
            var calculatedIngredients = _ingredients.Select(i => i.CalculateMacros());

            _calories = (int)calculatedIngredients.Sum(i => i.Calories);
            _protein = (int)calculatedIngredients.Sum(i => i.Protein);
            _carbs = (int)calculatedIngredients.Sum(i => i.Carbs);
            _fats = (int)calculatedIngredients.Sum(i => i.Fats);
            _fiber = (int)calculatedIngredients.Sum(i => i.Fiber);
            _sugar = (int)calculatedIngredients.Sum(i => i.Sugar);

            OnPropertyChanged(nameof(TotalCalories));
            OnPropertyChanged(nameof(TotalProtein));
            OnPropertyChanged(nameof(TotalCarbs));
            OnPropertyChanged(nameof(TotalFats));
            OnPropertyChanged(nameof(TotalFiber));
            OnPropertyChanged(nameof(TotalSugar));
        }

        public int TotalCalories => _calories;
        public int TotalProtein => _protein;
        public int TotalCarbs => _carbs;
        public int TotalFats => _fats;
        public int TotalFiber => _fiber;
        public int TotalSugar => _sugar;

        private void OnGoBack()
        {
            NavigationService.Instance.GoBack();
        }

        private async void OnAddDirection()
        {
            var dialog = new TextBox
            {
                PlaceholderText = "Enter direction step"
            };

            ContentDialog contentDialog = new ContentDialog
            {
                Title = "Add Direction",
                Content = dialog,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if (!string.IsNullOrWhiteSpace(dialog.Text))
                {
                    Directions.Add($"{Directions.Count + 1}. {dialog.Text}");
                }
            }
        }

        private async void OnAddIngredient()
        {
            var quantityBox = new TextBox { PlaceholderText = "Enter quantity" };
            var ingredientBox = new TextBox { PlaceholderText = "Enter ingredient name" };

            var panel = new StackPanel();
            panel.Children.Add(ingredientBox);
            panel.Children.Add(quantityBox);

            ContentDialog contentDialog = new ContentDialog
            {
                Title = "Add Ingredient",
                Content = panel,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if (!string.IsNullOrWhiteSpace(ingredientBox.Text) && !string.IsNullOrWhiteSpace(quantityBox.Text))
                {
                    if (float.TryParse(quantityBox.Text, out float quantity))
                    {
                        // Get ingredient from database
                        var ingredient = await _mealService.RetrieveIngredientByNameAsync(ingredientBox.Text);
                        if (ingredient != null)
                        {
                            var mealIngredient = new MealIngredient
                            {
                                IngredientId = ingredient.Id,
                                IngredientName = ingredient.Name,
                                Quantity = quantity,
                                Protein = ingredient.Protein,
                                Calories = ingredient.Calories,
                                Carbs = ingredient.Carbs,
                                Fats = ingredient.Fats,
                                Fiber = ingredient.Fiber,
                                Sugar = ingredient.Sugar
                            };

                            _ingredients.Add(mealIngredient);
                            OnPropertyChanged(nameof(IngredientNames));
                            CalculateTotalMacros();
                        }
                        else
                        {
                            // Show error that ingredient wasn't found
                            var errorDialog = new ContentDialog
                            {
                                Title = "Error",
                                Content = "Ingredient not found in database",
                                CloseButtonText = "OK",
                                XamlRoot = App.MainWindow.Content.XamlRoot
                            };
                            await errorDialog.ShowAsync();
                        }
                    }
                }
            }
        }

        private void OnSelectMealType(string mealType)
        {
            SelectedMealType = mealType;
        }

        private void OnSelectCookingLevel(string level)
        {
            SelectedCookingLevel = level;
        }

        public async Task<bool> CreateMealAsync(Meal meal)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_selectedCookingLevel))
                {
                    _selectedCookingLevel = "Beginner";
                }

                // Convert cooking time to integer
                if (!int.TryParse(_cookingTime, out int cookingTimeMinutes))
                {
                    cookingTimeMinutes = 0;
                }

                // Set all meal properties including calculated macros
                meal.Name = _mealName;
                meal.PreparationTime = cookingTimeMinutes;
                meal.CookingLevel = _selectedCookingLevel;
                meal.Calories = _calories;
                meal.Protein = _protein;
                meal.Carbohydrates = _carbs;
                meal.Fat = _fats;
                meal.Fiber = _fiber;
                meal.Sugar = _sugar;

                // Create the meal first
                int mealId = await _mealService.CreateMealAsync(meal);
                if (mealId > 0)
                {
                    // Then add the meal-ingredient relationships
                    foreach (var ingredient in _ingredients)
                    {
                        await _mealService.AddIngredientToMealAsync(mealId, ingredient.IngredientId, ingredient.Quantity);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating meal: {ex.Message}");
                return false;
            }
        }
    }
}