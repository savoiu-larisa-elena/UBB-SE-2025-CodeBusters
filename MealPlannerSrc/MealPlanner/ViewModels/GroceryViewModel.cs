using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using MealPlanner.Models;
using static System.Collections.Specialized.BitVector32;
using System.Diagnostics;
using System.Linq;

namespace MealPlanner.ViewModels
{
    public class GroceryViewModel : ObservableObject
    {
        public ObservableCollection<GroceryIngredient> Ingredients { get; } = new();
        public ObservableCollection<GroceryIngredient> MostFrequentIngredients { get; set; }
        public ObservableCollection<GroceryIngredient> RecentlyUsedIngredients { get; set; }

        private ObservableCollection<SectionModel> _sections;
        public ObservableCollection<SectionModel> Sections
        {
            get => _sections;
            set => SetProperty(ref _sections, value);
        }

        private string _newGroceryIngredientName;
        public string NewGroceryIngredientName
        {
            get => _newGroceryIngredientName;
            set => SetProperty(ref _newGroceryIngredientName, value);
        }

        //private int _newIngredientQuantity;
        //public int NewIngredientQuantity
        //{
        //    get => _newIngredientQuantity;
        //    set => SetProperty(ref _newIngredientQuantity, value);
        //}

        public RelayCommand<GroceryIngredient> AddGroceryIngredientCommand { get; }

        public GroceryViewModel()
        {
            AddGroceryIngredientCommand = new RelayCommand<GroceryIngredient>(AddGroceryIngredient);

            MostFrequentIngredients = new ObservableCollection<GroceryIngredient>
        {
            new GroceryIngredient { Name = "Tomatoes" },
            new GroceryIngredient { Name = "Onions" },
            new GroceryIngredient { Name = "Garlic" }
        };

            RecentlyUsedIngredients = new ObservableCollection<GroceryIngredient>
        {
            new GroceryIngredient { Name = "Olive Oil" },
            new GroceryIngredient { Name = "Salt" },
            new GroceryIngredient { Name = "Pepper" }
        };

            Sections = new ObservableCollection<SectionModel>
        {
            new SectionModel { Title = "Fruis & Veggies" },
            new SectionModel { Title = "Proteins" },
            new SectionModel { Title = "Carbohydrates" },
            new SectionModel { Title = "Pantry Essentials" },
            new SectionModel { Title = "Dairy & Alternatives" },
            new SectionModel { Title = "Snacks & Beverages" },
        };
        }

        public void AddGroceryIngredient(GroceryIngredient ingredient = null)
        {
            if (ingredient == null)
            {
                if (string.IsNullOrWhiteSpace(NewGroceryIngredientName))
                    return;

                ingredient = new GroceryIngredient
                {
                    Name = NewGroceryIngredientName,
                    //Quantity = NewIngredientQuantity
                };
            }

            if (!Ingredients.Any(i => i.Name == ingredient.Name))
            {
                Ingredients.Add(ingredient);
                Sections[0].Items.Add(ingredient);

                // TODO: Check the label from the database to categorize the new ingredient
            }

            NewGroceryIngredientName = string.Empty;
            // NewIngredientQuantity = 0;
        }

    }

}