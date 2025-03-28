using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlanner.Models
{
    public class Meal
    {
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public int Calories { get; set; }

        public string Category { get; set; }

        public string ImagePath { get; set; }

        public string Recipe { get; set; }

        public Meal(string name, string ingredients, int calories, string category, string imagePath, string recipe)
        {
            Name = name;
            Ingredients = ingredients;
            Calories = calories;
            Category = category;
            ImagePath = imagePath;
            Recipe = recipe;
        }

        public Meal() { }
    }
}