using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlanner.Models
{
    public class SectionModel
    {
        public string Title { get; set; }
        public ObservableCollection<GroceryIngredient> Items { get; set; } = new();
    }
}
