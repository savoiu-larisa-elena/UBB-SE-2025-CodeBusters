﻿using CommunityToolkit.Mvvm.Input;
using MealPlannerProject.Pages;
using MealPlannerProject.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MealPlannerProject.ViewModels
{
    public class DietaryPreferencesViewModel : INotifyPropertyChanged
    {
        public ICommand BackCommand { get; set; }
        public ICommand NextCommand { get; set; }

        private DietaryPreferencesService _dietaryPreferencesService = new DietaryPreferencesService();

        public ObservableCollection<string> OtherDietOptions { get; set; }
        public ObservableCollection<string> AllergenOptions { get; set; }

        private string _otherDiet;
        public string OtherDiet
        {
            get => _otherDiet;
            set
            {
                if (_otherDiet != value)
                {
                    _otherDiet = value;
                    OnPropertyChanged(nameof(OtherDiet));
                }
            }
        }

        private string _allergens;
        public string Allergens
        {
            get => _allergens;
            set
            {
                if (_allergens != value)
                {
                    _allergens = value;
                    OnPropertyChanged(nameof(Allergens));
                }
            }
        }

        public DietaryPreferencesViewModel()
        {
            BackCommand = new RelayCommand(BackAction);
            NextCommand = new RelayCommand(NextAction);

            // Populate options
            OtherDietOptions = new ObservableCollection<string>
        {
            "None", "Mediterranean", "Low-Fat", "Diabetic-Friendly", "Kosher", "Halal"
        };

            AllergenOptions = new ObservableCollection<string>
        {
            "None", "Peanuts", "Tree Nuts", "Dairy", "Eggs", "Gluten", "Shellfish", "Soy", "Fish", "Sesame"
        };
        }

        private void BackAction()
        {
            NavigationService.Instance.GoBack();
        }

        private void NextAction()
        {
            _dietaryPreferencesService.addAllergyAndDietaryPreference(FirstName, LastName, OtherDiet, Allergens);
            NavigationService.Instance.NavigateTo(typeof(YoureAllSetPage), this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string firstName;
        private string lastName;

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }



        public void SetUserInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

    }
}
