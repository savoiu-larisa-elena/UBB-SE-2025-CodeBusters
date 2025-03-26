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
using System.Diagnostics;

namespace MealPlanner.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("MainPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in MainPage constructor: {ex.Message}");
                throw; // Re-throw the exception to be caught by the global handler
            }
        }
    }
}
