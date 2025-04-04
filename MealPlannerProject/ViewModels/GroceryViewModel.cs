using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlannerProject.Models;
using MealPlannerProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.ViewModels
{
    public class GroceryViewModel : ObservableObject
    {

        private readonly GroceryListService _service = new();
        private readonly int _userId = 73;


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
                new SectionModel { Title = "My List" }
            };

            LoadUserGroceryList();
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
                    IsChecked = false
                };
            }

            if (Sections.SelectMany(s => s.Items).Any(i => i.Name == ingredient.Name))
                return;

            _service.AddIngredientToUser(_userId, ingredient);

            ingredient.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(GroceryIngredient.IsChecked))
                {
                    var ing = (GroceryIngredient)s;
                    _service.UpdateIsChecked(_userId, ing.Id, ing.IsChecked);
                }
            };

            Sections[0].Items.Add(ingredient);

            NewGroceryIngredientName = string.Empty;
        }


        private void LoadUserGroceryList()
        {
            var ingredientsFromDb = _service.GetIngredientsForUser(_userId);

            foreach (var ingredient in ingredientsFromDb)
            {
                ingredient.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(GroceryIngredient.IsChecked))
                    {
                        var item = (GroceryIngredient)s;
                        _service.UpdateIsChecked(_userId, item.Id, item.IsChecked);
                    }
                };
            }

            Sections = new ObservableCollection<SectionModel>
            {
                new SectionModel
                {
                    Title = "My List",
                    Items = new ObservableCollection<GroceryIngredient>(ingredientsFromDb)
                }
            };

            OnPropertyChanged(nameof(Sections));
        }
    }

}
