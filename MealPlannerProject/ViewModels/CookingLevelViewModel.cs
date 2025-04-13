namespace MealPlannerProject.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.Pages;
    using MealPlannerProject.Services;

    public partial class CookingLevelViewModel : ViewModelBase
    {
        public ObservableCollection<string> CookingLevels { get; } = new ObservableCollection<string>
                   {
                       "I am a beginner",
                       "I cook sometimes",
                       "I love cooking",
                       "I prefer quick meals",
                       "I meal prep",
                   };

        public ICommand BackCommand { get; }

        public ICommand NextCommand { get; }

        public CookingLevelViewModel()
        {
            this.BackCommand = new RelayCommand(this.GoBack);
            this.NextCommand = new RelayCommand(this.GoNext);

            this.firstName = string.Empty;
            this.lastName = string.Empty;
            this.selectedCookingSkill = string.Empty;

            this.PropertyChanged = (sender, args) => { };
        }

        private void GoBack()
        {
            NavigationService.Instance.GoBack();
        }

        private string firstName;
        private string lastName;

        public string FirstName
        {
            get => this.firstName;
            set
            {
                this.firstName = value;
                this.OnPropertyChanged(nameof(this.FirstName));
            }
        }

        public string LastName
        {
            get => this.lastName;
            set
            {
                this.lastName = value;
                this.OnPropertyChanged(nameof(this.LastName));
            }
        }

        public string SelectedCookingSkill
        {
            get => this.selectedCookingSkill;
            set
            {
                this.selectedCookingSkill = value;
                this.OnPropertyChanged(nameof(this.SelectedCookingSkill));
            }
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        private ICookingPageService cooking = new CookingPageService();
        private string selectedCookingSkill;

        private void GoNext()
        {
            this.cooking.AddCookingSkill(this.FirstName, this.LastName, this.SelectedCookingSkill);
            NavigationService.Instance.NavigateTo(typeof(YoureAllSetPage), this);
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected new void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
