namespace MealPlanner.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Windows.System;

    [Table("daily_meals")]
    public class Macros
    {
        [Key]
        [ForeignKey("User")]
        public int U_Id { get; set; } // Foreign key from Users table

        public float TotalProtein { get; set; } // Total protein consumed

        public float TotalCarbohydrates { get; set; } // Total carbs consumed

        public float TotalFat { get; set; } // Total fat consumed

        public float TotalFiber { get; set; } // Total fiber consumed

        public float TotalSugar { get; set; } // Total sugar consumed


        public virtual User? User { get; set; } // Navigation property
    }
}
