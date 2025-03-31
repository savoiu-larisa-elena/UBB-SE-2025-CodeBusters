using MealPlanner.Pages;
using MealPlanner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using System.Collections.ObjectModel;
using MealPlanner.Pages;
using MealPlanner.Models;
using Microsoft.Data.SqlClient;


namespace MealPlanner.ViewModels
{
    public class BodyMetricsViewModel : ViewModelBase
    {
        public BodyMetricsModel BodyMetrics { get; set; } = new BodyMetricsModel();
        public ICommand NextCommand { get; }
        public ICommand SaveCommand { get; }

        public BodyMetricsViewModel()
        {
            NextCommand = new RelayCommand(GoNext);
            SaveCommand = new RelayCommand(SaveToDatabase);
        }

        private void GoNext()
        {
            SaveToDatabase();
            NavigationService.Instance.NavigateTo(typeof(GoalPage));
        }

        private void SaveToDatabase()
        {
            string connectionString = @"Server=RALUUU_LPT\SQLEXPRESS;Database=MealPlanner;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO body_metrics (height, weight, target_weight) 
                    VALUES (@height, @weight, @targetWeight);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@height", BodyMetrics.Height);
                    command.Parameters.AddWithValue("@weight", BodyMetrics.Weight);
                    command.Parameters.AddWithValue("@targetWeight", BodyMetrics.TargetWeight);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}