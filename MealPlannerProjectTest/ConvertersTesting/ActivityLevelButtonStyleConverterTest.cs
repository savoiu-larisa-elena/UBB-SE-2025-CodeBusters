using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Converters;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;

namespace MealPlannerProjectTest.ConverterTesting
{
    [TestClass]
    public class ActivityLevelButtonStyleConverterTests
    {
        private ActivityLevelButtonStyleConverter? _converter;

        [TestInitialize]
        public void Setup()
        {
            _converter = new ActivityLevelButtonStyleConverter();

            // Setup fake resources for test environment
            var resources = new ResourceDictionary
            {
                ["SedentaryButtonStyle"] = new Style(),
                ["LightlyActiveButtonStyle"] = new Style(),
                ["ModeratelyActiveButtonStyle"] = new Style(),
                ["VeryActiveButtonStyle"] = new Style(),
                ["SuperActiveButtonStyle"] = new Style()
            };

            Application.Start((p) => new Microsoft.UI.Xaml.Application { Resources = resources });
        }

        [TestMethod]
        public void Convert_Sedentary_ReturnsCorrectStyle()
        {
            var result = _converter.Convert("Sedentary", typeof(Style), null, "en-US");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Style));
        }

        [TestMethod]
        public void Convert_LightlyActive_ReturnsCorrectStyle()
        {
            var result = _converter.Convert("Lightly Active", typeof(Style), null, "en-US");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Convert_UnknownValue_ReturnsNull()
        {
            var result = _converter.Convert("Unrealistically Active", typeof(Style), null, "en-US");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Convert_NonStringValue_ReturnsNull()
        {
            var result = _converter.Convert(42, typeof(Style), null, "en-US");
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBack_Always_ThrowsNotImplementedException()
        {
            _converter.ConvertBack(new object(), typeof(string), null, "en-US");
        }
    }
}
