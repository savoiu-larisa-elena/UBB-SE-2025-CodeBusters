namespace MealPlannerProject.Models
{
    using System;

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

        public DateTime CreatedAt { get; set; }

        public string CookingLevel { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; internal set; }

        public Meal(string name, string ingredients, int calories, string category, string photoLink, string recipe)
        {
            this.Name = name;
            this.Ingredients = ingredients;
            this.Calories = calories;
            this.Category = category;
            this.PhotoLink = photoLink;
            this.Recipe = recipe;
            this.CreatedAt = DateTime.Now;
        }

        public Meal()
        {
            this.CreatedAt = DateTime.Now;
        }
    }
}