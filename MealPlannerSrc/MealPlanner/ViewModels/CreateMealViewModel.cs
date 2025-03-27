using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.UI.Xaml.Controls;
using MealPlanner.Services;
using System;

namespace MealPlanner.ViewModels
{
    public class CreateMealViewModel : ViewModelBase
    {
        private string _mealName;
        private string _cookingTime;
        private string _selectedMealType;
        private string _selectedCookingLevel;
        private ObservableCollection<string> _directions;
        private ObservableCollection<string> _ingredients;

        public CreateMealViewModel()
        {
            Directions = new ObservableCollection<string>();
            Ingredients = new ObservableCollection<string>();
            GoBackCommand = new RelayCommand(OnGoBack);
            AddDirectionCommand = new RelayCommand(OnAddDirection);
            AddIngredientCommand = new RelayCommand(OnAddIngredient);
            SelectMealTypeCommand = new RelayCommand(() => OnSelectMealType(null));
            SelectCookingLevelCommand = new RelayCommand(() => OnSelectCookingLevel(null));
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

        public ObservableCollection<string> Ingredients
        {
            get => _ingredients;
            set
            {
                if (_ingredients != value)
                {
                    _ingredients = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand GoBackCommand { get; }
        public ICommand AddDirectionCommand { get; }
        public ICommand AddIngredientCommand { get; }
        public ICommand SelectMealTypeCommand { get; }
        public ICommand SelectCookingLevelCommand { get; }

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
                CloseButtonText = "Cancel"
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
            var dialog = new TextBox
            {
                PlaceholderText = "Enter ingredient"
            };

            ContentDialog contentDialog = new ContentDialog
            {
                Title = "Add Ingredient",
                Content = dialog,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel"
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if (!string.IsNullOrWhiteSpace(dialog.Text))
                {
                    Ingredients.Add($"• {dialog.Text}");
                }
            }
        }

        private void OnSelectMealType(object mealType)
        {
            SelectedMealType = mealType as string;
        }

        private void OnSelectCookingLevel(object level)
        {
            SelectedCookingLevel = level as string;
        }
    }
}
