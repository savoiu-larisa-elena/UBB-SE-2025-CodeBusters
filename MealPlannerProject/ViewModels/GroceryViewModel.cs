namespace MealPlannerProject.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using MealPlannerProject.Models;
    using MealPlannerProject.Services;

    public class GroceryViewModel : ObservableObject
    {
        public static int UserId;
        private readonly GroceryListService service = new ();

        public ObservableCollection<GroceryIngredient> Ingredients { get; } = new ();

        public ObservableCollection<GroceryIngredient> MostFrequentIngredients { get; set; }

        public ObservableCollection<GroceryIngredient> RecentlyUsedIngredients { get; set; }

        private ObservableCollection<SectionModel> sections;

        private string newGroceryIngredientName;

        public ObservableCollection<SectionModel> Sections
        {
            get => this.sections;
            set => this.SetProperty(ref this.sections, value);
        }

        public string NewGroceryIngredientName
        {
            get => this.newGroceryIngredientName;
            set => this.SetProperty(ref this.newGroceryIngredientName, value);
        }

        // private int _newIngredientQuantity;
        // public int NewIngredientQuantity
        // {
        //    get => _newIngredientQuantity;
        //    set => SetProperty(ref _newIngredientQuantity, value);
        // }
        public RelayCommand<GroceryIngredient> AddGroceryIngredientCommand { get; }

        public GroceryViewModel()
        {
            //this.userId = userId;
            this.AddGroceryIngredientCommand = new RelayCommand<GroceryIngredient>(this.AddGroceryIngredient);

            this.MostFrequentIngredients = new ObservableCollection<GroceryIngredient>
            {
                new GroceryIngredient { Name = "Tomatoes" },
                new GroceryIngredient { Name = "Onions" },
                new GroceryIngredient { Name = "Garlic" },
            };

            this.RecentlyUsedIngredients = new ObservableCollection<GroceryIngredient>
            {
                new GroceryIngredient { Name = "Olive Oil" },
                new GroceryIngredient { Name = "Salt" },
                new GroceryIngredient { Name = "Pepper" },
            };

            this.Sections = new ObservableCollection<SectionModel>
            {
                new SectionModel { Title = "My List" },
            };

            this.LoadUserGroceryList();
        }

        public void AddGroceryIngredient(GroceryIngredient? ingredient = null)
        {
            if (ingredient == null)
            {
                if (string.IsNullOrWhiteSpace(this.NewGroceryIngredientName))
                {
                    return;
                }

                ingredient = new GroceryIngredient
                {
                    Name = this.NewGroceryIngredientName,
                    IsChecked = false,
                };
            }

            if (this.Sections.SelectMany(s => s.Items).Any(i => i.Name == ingredient.Name))
            {
                return;
            }

            this.service.AddIngredientToUser(UserId, ingredient);

            ingredient.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(GroceryIngredient.IsChecked))
                {
                    var ing = (GroceryIngredient)s;
                    this.service.UpdateIsChecked(UserId, ing.Id, ing.IsChecked);
                }
            };

            this.Sections[0].Items.Add(ingredient);

            this.NewGroceryIngredientName = string.Empty;
        }


        private void LoadUserGroceryList()
        {
            var ingredientsFromDb = this.service.GetIngredientsForUser(UserId);

            foreach (var ingredient in ingredientsFromDb)
            {
                ingredient.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(GroceryIngredient.IsChecked))
                    {
                        var item = (GroceryIngredient)s;
                        this.service.UpdateIsChecked(UserId, item.Id, item.IsChecked);
                    }
                };
            }

            this.Sections = new ObservableCollection<SectionModel>
            {
                new SectionModel
                {
                    Title = "My List",
                    Items = new ObservableCollection<GroceryIngredient>(ingredientsFromDb),
                },
            };

            this.OnPropertyChanged(nameof(this.Sections));
        }
    }

}
