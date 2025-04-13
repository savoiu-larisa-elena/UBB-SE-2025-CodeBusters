namespace MealPlannerProject.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Windows.System;

    [Table("calorie_trackers")]
    public class Calorie
    {
        [Key]
        [ForeignKey("User")]
        public int U_Id { get; set; } // Foreign key to Users table

        public DateTime Today { get; set; } // Date of the tracking record

        public float DailyIntake { get; set; } // Goal calories for the day

        public float CaloriesConsumed { get; set; } // Food calories consumed

        public float CaloriesBurned { get; set; } // Exercise calories burned

        public virtual User? User { get; set; } // Navigation property to User

    }
}
