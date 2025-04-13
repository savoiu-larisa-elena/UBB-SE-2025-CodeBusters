using MealPlannerProject.Converters;
using Microsoft.UI.Xaml;
using Xunit;

namespace MealPlannerProject.Tests
{
    public class CookingLevelButtonStyleConverterTests
    {
        private readonly CookingLevelButtonStyleConverter _converter = new CookingLevelButtonStyleConverter();

        [Theory]
        [InlineData("I'm a beginner", "BeginnerButtonStyle")]
        [InlineData("I cook sometimes", "CookSometimesButtonStyle")]
        [InlineData("I love cooking", "LoveCookingButtonStyle")]
        [InlineData("I prefer quick meals", "QuickMealsButtonStyle")]
        [InlineData("I meal prep", "MealPrepButtonStyle")]
        public void Convert_ValidCookingLevel_ReturnsCorrectStyle(string cookingLevel, string expectedStyle)
        {
            var result = _converter.Convert(cookingLevel, typeof(Style), null, null);
            var expected = Application.Current.Resources[expectedStyle] as Style;

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Unknown cooking level")]
        [InlineData(null)]
        public void Convert_InvalidCookingLevel_ReturnsNull(string cookingLevel)
        {
            var result = _converter.Convert(cookingLevel, typeof(Style), null, null);
            Assert.Null(result);
        }
    }
}
