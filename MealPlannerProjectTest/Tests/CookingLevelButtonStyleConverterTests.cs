using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.UI.Xaml;
using System;
using System.Reflection;

namespace MealPlannerProjectTest.Converters
{
    [TestClass]
    public class CookingLevelButtonStyleConverterTests
    {
        private object? _converter;
        private Type? _converterType;

        [TestInitialize]
        public void Setup()
        {
            _converterType = Type.GetType("MealPlannerProject.Converters.CookingLevelButtonStyleConverter, MealPlannerProject");
            if (_converterType == null)
            {
                throw new Exception("Could not find CookingLevelButtonStyleConverter type");
            }

            _converter = Activator.CreateInstance(_converterType);
        }

        #region Convert Tests

        [DataTestMethod]
        [DataRow("I'm a beginner", "BeginnerButtonStyle")]
        [DataRow("I cook sometimes", "CookSometimesButtonStyle")]
        [DataRow("I love cooking", "LoveCookingButtonStyle")]
        [DataRow("I prefer quick meals", "QuickMealsButtonStyle")]
        [DataRow("I meal prep", "MealPrepButtonStyle")]
        public void Convert_ValidCookingLevel_ReturnsExpectedStyle(string cookingLevel, string expectedStyleKey)
        {
            // Arrange
            var method = _converterType!.GetMethod("Convert");
            Assert.IsNotNull(method, "Convert method not found");

            var expectedStyle = Application.Current.Resources[expectedStyleKey] as Style;

            // Act
            var result = method!.Invoke(_converter, new object[] { cookingLevel, typeof(Style), null!, null! });

            // Assert
            Assert.AreEqual(expectedStyle, result);
        }

        [DataTestMethod]
        [DataRow("Unknown cooking level")]
        [DataRow(null)]
        public void Convert_InvalidCookingLevel_ReturnsNull(object? cookingLevel)
        {
            // Arrange
            var method = _converterType!.GetMethod("Convert");
            Assert.IsNotNull(method, "Convert method not found");

            // Act
            var result = method!.Invoke(_converter, new object[] { cookingLevel, typeof(Style), null!, null! });

            // Assert
            Assert.IsNull(result);
        }

        #endregion
    }
}
