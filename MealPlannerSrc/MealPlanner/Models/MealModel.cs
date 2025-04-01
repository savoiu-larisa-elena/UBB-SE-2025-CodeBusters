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

        public int Protein { get; set; }

        public int Carbohydrates { get; set; }

        public int Fat { get; set; }

        public int Fiber { get; set; }

        public int Sugar { get; set; }

        public string PhotoLink { get; set; }

        public string Recipe { get; set; }

        public int PreparationTime { get; set; }

        public int Servings { get; set; }

        public Meal(string name, string ingredients, int calories, string category, string photoLink, string recipe)
        {
            Name = name;
            Ingredients = ingredients;
            Calories = calories;
            Category = category;
            PhotoLink = photoLink;
            Recipe = recipe;
        }

        public Meal() { }
    }
}