namespace MealPlannerProject.Pages
{
    using MealPlannerProject.ViewModels;
    using Microsoft.UI.Xaml.Controls;

    public sealed partial class MealListPage : Page
    {
        public MealListPage()
        {
            this.InitializeComponent();
            this.DataContext = new MealListViewModel();
        }
    }
}