using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MealPlannerProject.Pages;
using MealPlannerProject.Services;

namespace MealPlannerProject.ViewModels
{
    public partial class CookingLevelViewModel : ViewModelBase
    {
        public ObservableCollection<string> CookingLevels { get; } = new ObservableCollection<string>
        {
            "I am a beginner",
            "I cook sometimes",
            "I love cooking",
            "I prefer quick meals",
            "I meal prep"
        };

        public ICommand BackCommand { get; }
        public ICommand NextCommand { get; }

        public CookingLevelViewModel()
        {
            BackCommand = new RelayCommand(GoBack);
            NextCommand = new RelayCommand(GoNext);
        }

        private void GoBack()
        {
            NavigationService.Instance.GoBack();
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

        public string SelectedCookingSkill
        {
            get => _selectedCookingSkill;
            set
            {
                _selectedCookingSkill = value;
                OnPropertyChanged(nameof(SelectedCookingSkill));
            }
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }


        private CookingPageService cooking = new CookingPageService();
        private string _selectedCookingSkill;

        private void GoNext()
        {
            cooking.addCookingSkill(FirstName, LastName, SelectedCookingSkill);
            NavigationService.Instance.NavigateTo(typeof(YoureAllSetPage), this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
