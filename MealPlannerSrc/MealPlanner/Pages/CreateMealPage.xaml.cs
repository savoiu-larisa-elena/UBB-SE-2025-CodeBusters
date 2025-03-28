using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using System;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using MealPlanner.Models;
using MealPlanner.Services;
using MealPlanner.ViewModels;

namespace MealPlanner.Pages
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

            // Get the window handle
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            
            // Initialize the picker with the window handle
            InitializeWithWindow.Initialize(picker, hwnd);

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
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
                if (string.IsNullOrWhiteSpace(_viewModel.MealName) || string.IsNullOrWhiteSpace(_viewModel.SelectedMealType))
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Validation Error",
                        Content = "Please enter a meal name and select a meal type.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };

                    await dialog.ShowAsync();
                    return;
                }

                var meal = new Meal
                {
                    Name = _viewModel.MealName,
                    Recipe = string.Join("\n", _viewModel.Directions),
                    Category = _viewModel.SelectedMealType,
                    Calories = 426,
                    Ingredients = string.Join("\n", _viewModel.Ingredients)
                };

                bool success = await _viewModel.CreateMealAsync(meal);

                if (success)
                {
                    // Refresh the meal list
                    await App.MealListViewModel.LoadMealsAsync();

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
            // Navigate to the MainPage
            Frame.Navigate(typeof(MainPage));
        }
    }
}
