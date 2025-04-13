using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MealPlannerProject.Interfaces.Services;
using MealPlannerProject.Models;
using MealPlannerProject.Queries;
using MealPlannerProject.Repositories;
using MealPlannerProject.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlannerProjectTest.Services
{
    [TestClass]
    public class MealServiceTests
    {
        private Mock<MealRepository> _mockMealRepository;
        private Mock<IngredientRepository> _mockIngredientRepository;
        private MealService _mealService;

        [TestInitialize]
        public void Setup()
        {
            _mockMealRepository = new Mock<MealRepository>();
            _mockIngredientRepository = new Mock<IngredientRepository>();

            // Use reflection to set the private repository fields
            var mealService = new MealService();
            var mealRepoField = typeof(MealService).GetField("mealDatabaseRepository", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var ingredientRepoField = typeof(MealService).GetField("ingredientDatabaseRepository", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            mealRepoField.SetValue(mealService, _mockMealRepository.Object);
            ingredientRepoField.SetValue(mealService, _mockIngredientRepository.Object);

            _mealService = mealService;
        }

        #region ResolveMealTypeIdentifier Tests

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenBreakfast_ReturnsBreakfastId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveMealTypeIdentifier", "breakfast");

            // Assert
            Assert.AreEqual(1, result); // BreakfastTypeId
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenLunch_ReturnsLunchId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveMealTypeIdentifier", "lunch");

            // Assert
            Assert.AreEqual(2, result); // LunchTypeId
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenDinner_ReturnsDinnerId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveMealTypeIdentifier", "dinner");

            // Assert
            Assert.AreEqual(3, result); // DinnerTypeId
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenSnack_ReturnsSnackId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveMealTypeIdentifier", "snack");

            // Assert
            Assert.AreEqual(4, result); // SnackTypeId
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenDessert_ReturnsDessertId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveMealTypeIdentifier", "dessert");

            // Assert
            Assert.AreEqual(5, result); // DessertTypeId
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenPostWorkout_ReturnsPostWorkoutId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveMealTypeIdentifier", "post-workout");

            // Assert
            Assert.AreEqual(6, result); // PostWorkoutTypeId
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenPreWorkout_ReturnsPreWorkoutId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveMealTypeIdentifier", "pre-workout");

            // Assert
            Assert.AreEqual(7, result); // PreWorkoutTypeId
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenVeganMeal_ReturnsVeganMealId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveMealTypeIdentifier", "vegan meal");

            // Assert
            Assert.AreEqual(8, result); // VeganMealTypeId
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenHighProteinMeal_ReturnsHighProteinMealId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveMealTypeIdentifier", "high-protein meal");

            // Assert
            Assert.AreEqual(9, result); // HighProteinMealTypeId
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenLowCarbMeal_ReturnsLowCarbMealId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveMealTypeIdentifier", "low-carb meal");

            // Assert
            Assert.AreEqual(10, result); // LowCarbMealTypeId
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenUnknown_ReturnsDefaultId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveMealTypeIdentifier", "unknown");

            // Assert
            Assert.AreEqual(1, result); // DefaultMealTypeId
        }

        #endregion

        #region ResolveCookingSkillIdentifier Tests

        [TestMethod]
        public void ResolveCookingSkillIdentifier_WhenBeginner_ReturnsBeginnerId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveCookingSkillIdentifier", "beginner");

            // Assert
            Assert.AreEqual(1, result); // BeginnerSkillId
        }

        [TestMethod]
        public void ResolveCookingSkillIdentifier_WhenIntermediate_ReturnsIntermediateId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveCookingSkillIdentifier", "intermediate");

            // Assert
            Assert.AreEqual(2, result); // IntermediateSkillId
        }

        [TestMethod]
        public void ResolveCookingSkillIdentifier_WhenAdvanced_ReturnsAdvancedId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveCookingSkillIdentifier", "advanced");

            // Assert
            Assert.AreEqual(3, result); // AdvancedSkillId
        }

        [TestMethod]
        public void ResolveCookingSkillIdentifier_WhenUnknown_ReturnsDefaultId()
        {
            // Act
            int result = InvokePrivateMethod<int>(_mealService, "ResolveCookingSkillIdentifier", "unknown");

            // Assert
            Assert.AreEqual(1, result); // DefaultCookingSkillId
        }

        #endregion

        #region RetrieveAllMealsAsync Tests

        [TestMethod]
        public async Task RetrieveAllMealsAsync_WhenSuccessful_ReturnsMealsList()
        {
            // This test would normally use a mock for DataLink.Instance
            // However, since that's a singleton with a private constructor, we'd need to refactor the code
            // or use a mocking framework that can mock static properties
            // For this example, we'll create an integration test

            // This is a placeholder for how you would approach this test if we refactored the code
            // to allow proper dependency injection for DataLink

            /*
            // Arrange
            var mockDataTable = new DataTable();
            mockDataTable.Columns.Add("m_name", typeof(string));
            mockDataTable.Columns.Add("recipe", typeof(string));
            mockDataTable.Columns.Add("meal_type", typeof(string));
            mockDataTable.Columns.Add("calories", typeof(int));
            mockDataTable.Columns.Add("preparation_time", typeof(int));
            mockDataTable.Columns.Add("servings", typeof(int));
            mockDataTable.Columns.Add("protein", typeof(int));
            mockDataTable.Columns.Add("carbohydrates", typeof(int));
            mockDataTable.Columns.Add("fat", typeof(int));
            mockDataTable.Columns.Add("fiber", typeof(int));
            mockDataTable.Columns.Add("sugar", typeof(int));
            mockDataTable.Columns.Add("photo_link", typeof(string));
            mockDataTable.Columns.Add("cooking_level", typeof(string));
            
            mockDataTable.Rows.Add("Spaghetti", "Cook pasta", "dinner", 500, 30, 4, 20, 60, 15, 5, 3, "photo.jpg", "beginner");
            mockDataTable.Rows.Add("Salad", "Mix ingredients", "lunch", 200, 10, 2, 5, 15, 10, 8, 2, "salad.jpg", "beginner");
            
            _mockDataLink.Setup(m => m.ExecuteReader(It.IsAny<string>()))
                .Returns(mockDataTable);
            
            // Act
            var result = await _mealService.RetrieveAllMealsAsync();
            
            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Spaghetti", result[0].Name);
            Assert.AreEqual("Salad", result[1].Name);
            */

            // For now, we'll just verify the method returns a list (even empty)
            // Act
            var result = await _mealService.RetrieveAllMealsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Meal>));
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Helper method to invoke private methods using reflection
        /// </summary>
        private T InvokePrivateMethod<T>(object instance, string methodName, params object[] parameters)
        {
            var methodInfo = instance.GetType().GetMethod(methodName,
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (methodInfo == null)
                throw new ArgumentException($"Method {methodName} not found");

            return (T)methodInfo.Invoke(instance, parameters);
        }

        #endregion
    }
}
