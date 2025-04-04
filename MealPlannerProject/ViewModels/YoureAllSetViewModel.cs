using MealPlannerProject.Pages;
using MealPlannerProject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MealPlannerProject.ViewModels
{
    public class YoureAllSetViewModel
    {
        public ICommand NextCommand { get; }

        public YoureAllSetViewModel()
        {
            NextCommand = new RelayCommand(GoNext);
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GoNext()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage), this);
        }
    }
}
