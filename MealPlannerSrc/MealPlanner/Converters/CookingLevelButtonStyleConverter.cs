using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace MealPlanner.Converters
{
    public class CookingLevelButtonStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string cookingLevel)
            {
                switch (cookingLevel)
                {
                    case "I'm a beginner":
                        return Application.Current.Resources["BeginnerButtonStyle"] as Style;
                    case "I cook sometimes":
                        return Application.Current.Resources["CookSometimesButtonStyle"] as Style;
                    case "I love cooking":
                        return Application.Current.Resources["LoveCookingButtonStyle"] as Style;
                    case "I prefer quick meals":
                        return Application.Current.Resources["QuickMealsButtonStyle"] as Style;
                    case "I meal prep":
                        return Application.Current.Resources["MealPrepButtonStyle"] as Style;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
