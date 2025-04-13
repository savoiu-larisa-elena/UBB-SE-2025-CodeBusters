namespace MealPlannerProject.Converters
{
    using System;
    using Microsoft.UI;
    using Microsoft.UI.Xaml.Data;
    using Microsoft.UI.Xaml.Media;
    using Windows.UI;

    public class GoalButtonColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch (value.ToString())
            {
                case "Lose weight": return new SolidColorBrush(Color.FromArgb(255, 82, 115, 91));
                case "Gain weight": return new SolidColorBrush(Color.FromArgb(255, 186, 104, 72));
                case "Maintain weight": return new SolidColorBrush(Color.FromArgb(255, 25, 50, 38));
                case "Body recomposition": return new SolidColorBrush(Color.FromArgb(255, 238, 217, 195));
                case "Improve overall health": return new SolidColorBrush(Color.FromArgb(255, 176, 135, 94));
                default: return new SolidColorBrush(Colors.White);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
