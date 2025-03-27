using Microsoft.UI.Xaml;
using System;

using Microsoft.Data.SqlClient;
using MealPlanner;
public class DatabaseConfig
{
    public static string ConnectionString = @"Server=DESKTOP-H700VKM\MSSQLSERVER01;Database=MealPlanner;Integrated Security=True;TrustServerCertificate=True";
}


namespace MealPlanner
{
    public partial class App : Application
    {
        public Window Window { get; set; }

        public App()
        {
            this.InitializeComponent();
            this.UnhandledException += OnUnhandledException;
            if(!DatabaseHelper.TestConnection())
            {
                throw new Exception("Database connection failed.");
            }
        }

        private void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // Log the exception details
            System.Diagnostics.Debug.WriteLine($"Unhandled exception: {e.Message}");
            if (e.Exception != null)
            {
                System.Diagnostics.Debug.WriteLine($"Exception stack trace: {e.Exception.StackTrace}");
            }
            e.Handled = true; // Prevent the application from closing
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Window = new MainWindow();
            Window.Activate();
        }
    }
}
