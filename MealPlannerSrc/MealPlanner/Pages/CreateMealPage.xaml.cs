using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using System;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;

namespace MealPlanner.Pages
{
    public sealed partial class CreateMealPage : Page
    {
        public CreateMealPage()
        {
            this.InitializeComponent();
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
            var window = (Application.Current as App)?.Window as Microsoft.UI.Xaml.Window;
            var hwnd = WindowNative.GetWindowHandle(window);
            
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement save functionality here
            // For now, just navigate to the MainPage
            Frame.Navigate(typeof(MainPage));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the MainPage
            Frame.Navigate(typeof(MainPage));
        }
    }
}
