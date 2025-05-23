﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Windows.System;

namespace MealPlannerProject.Models
{
    [Table("water_trackers")]
    public class Water
    {
        [Key]
        [ForeignKey("User")]
        public int U_Id { get; set; }  // Foreign key from Users table

        public float WaterIntake { get; set; }  // Amount of water consumed

        public virtual User User { get; set; }  // Navigation property
    }
}
