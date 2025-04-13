namespace MealPlannerProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SectionModel
    {
        required public string Title { get; set; }

        public ObservableCollection<GroceryIngredient> Items { get; set; } = new ();
    }
}
