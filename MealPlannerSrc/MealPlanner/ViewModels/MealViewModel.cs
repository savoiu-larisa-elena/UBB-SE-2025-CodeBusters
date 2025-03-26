using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlanner.ViewModels
{
    public class MealViewModel : INotifyPropertyChanged
    {
        private string mealName;
        private string cookingTime;
        private ObservableCollection<string> directions;
        private ObservableCollection<string> ingredients;

        public string MealName
        {
            get => mealName;
            set
            {
                if (mealName != value)
                {
                    mealName = value;
                    OnPropertyChanged(nameof(MealName));
                }
            }
        }

        public string CookingTime
        {
            get => cookingTime;
            set
            {
                if (cookingTime != value)
                {
                    cookingTime = value;
                    OnPropertyChanged(nameof(CookingTime));
                }
            }
        }

        public ObservableCollection<string> Directions
        {
            get => directions;
            set
            {
                if (directions != value)
                {
                    directions = value;
                    OnPropertyChanged(nameof(Directions));
                }
            }
        }

        public ObservableCollection<string> Ingredients
        {
            get => ingredients;
            set
            {
                if (ingredients != value)
                {
                    ingredients = value;
                    OnPropertyChanged(nameof(Ingredients));
                }
            }
        }

        public MealViewModel()
        {
            Directions = new ObservableCollection<string>();
            Ingredients = new ObservableCollection<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
