using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using System;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using MealPlannerProject.Models;
using MealPlannerProject.Services;
using MealPlannerProject.ViewModels;
using Windows.Storage;

namespace MealPlannerProject.Pages
{
    public sealed partial class CreateMealPage : Page
    {
        private readonly CreateMealViewModel _viewModel;

        public CreateMealPage()
        {
            this.InitializeComponent();
            _viewModel = new CreateMealViewModel();
            this.DataContext = _viewModel;
        }

        private async void ChoosePictureButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(picker, hwnd);

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                _viewModel.SelectedImage = file;
                var bitmapImage = new BitmapImage();
                using (var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    await bitmapImage.SetSourceAsync(stream);
                }
                SelectedImage.Source = bitmapImage;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(_viewModel.MealName) ||
                    string.IsNullOrWhiteSpace(_viewModel.SelectedMealType) ||
                    string.IsNullOrWhiteSpace(_viewModel.CookingTime))
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Validation Error",
                        Content = "Please enter all required fields: meal name, meal type, and cooking time.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };

                    await dialog.ShowAsync();
                    return;
                }

                // Create meal with all required properties
                var meal = new Meal
                {
                    Name = _viewModel.MealName,
                    Recipe = _viewModel.Directions.Count > 0 ? string.Join("\n", _viewModel.Directions) : "No directions provided",
                    Category = _viewModel.SelectedMealType ?? "Other",
                    CookingLevel = _viewModel.SelectedCookingLevel ?? "Beginner",
                    PreparationTime = int.TryParse(_viewModel.CookingTime, out int time) ? time : 0,
                    PhotoLink = _viewModel.SelectedImage?.Path ?? "default.jpg",
                    Calories = _viewModel.TotalCalories,
                    Protein = _viewModel.TotalProtein,
                    Carbohydrates = _viewModel.TotalCarbs,
                    Fat = _viewModel.TotalFats,
                    Fiber = _viewModel.TotalFiber,
                    Sugar = _viewModel.TotalSugar,
                    CreatedAt = DateTime.Now
                };

                bool success = await _viewModel.CreateMealAsync(meal);

                if (success)
                {
                    

                    var dialog = new ContentDialog
                    {
                        Title = "Success",
                        Content = "Meal saved successfully!",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };

                    await dialog.ShowAsync();
                    Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "Failed to save meal. Please try again.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };

                    await dialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving meal: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = $"An error occurred: {ex.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                await dialog.ShowAsync();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}