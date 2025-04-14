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
        private static int _userId;
        public static int UserId
        {
            get => _userId;
            set => _userId =  value;
        }
        private readonly GroceryListService service = new ();

        public ObservableCollection<GroceryIngredient> Ingredients { get; private set; } = new ();

        public ObservableCollection<GroceryIngredient> MostFrequentIngredients { get; private set; }

        public ObservableCollection<GroceryIngredient> RecentlyUsedIngredients { get; private set; }

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
            this.sections = new ();
            this.newGroceryIngredientName = "";

            this.LoadUserGroceryList();
        }

        public void AddGroceryIngredient(GroceryIngredient? ingredient = null)
        {
            GroceryIngredient resultIngredient = this.service.AddIngredientToUser(_userId, ingredient ?? GroceryIngredient.defaultIngredient, newGroceryIngredientName, sections);
            if (resultIngredient == GroceryIngredient.defaultIngredient)
                return;
            else
                ingredient = resultIngredient;

            this.Sections[0].Items.Add(ingredient);

            this.NewGroceryIngredientName = string.Empty;
        }


        private void LoadUserGroceryList()
        {
            var ingredientsFromDb = this.service.GetIngredientsForUser(_userId);

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
