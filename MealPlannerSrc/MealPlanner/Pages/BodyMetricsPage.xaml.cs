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

    public sealed partial class BodyMetricsPage : Page
    {
        public BodyMetricsPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("BodyMetricsPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in BodyMetricsPage constructor: {ex.Message}");
                throw; 
            }
        }
    }
}
