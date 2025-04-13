namespace MealPlannerProject.ViewModels
{
    using System.ComponentModel;
    using System.Windows.Input;
    using MealPlannerProject.Pages;
    using MealPlannerProject.Services;

    public class YoureAllSetViewModel
    {
        public ICommand NextCommand { get; }

        private string firstName;
        private string lastName;

        public YoureAllSetViewModel()
        {
            this.NextCommand = new RelayCommand(this.GoNext);
            this.firstName = string.Empty;
            this.lastName = string.Empty;
        }

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

        public void SetUserInfo(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GoNext()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage), this);
        }
    }
}
