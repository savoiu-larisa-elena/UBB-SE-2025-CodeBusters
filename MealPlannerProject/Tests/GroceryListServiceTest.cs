using Moq;
using Xunit;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MealPlannerProject.Models;
using MealPlannerProject.Services;
using MealPlannerProject.Queries;

namespace MealPlannerProject.Tests
{
    public class GroceryListServiceTests
    {
        private readonly Mock<DataLink> _mockDataLink;  // Mock for the DataLink static class
        private readonly IGroceryListService _groceryListService;

        public GroceryListServiceTests()
        {
            // Create a mock instance of DataLink (or wrap the static calls)
            _mockDataLink = new Mock<DataLink>();

            // Setup your mock DataLink methods here:
            _mockDataLink.Setup(dl => dl.ExecuteReader(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
                         .Returns(new DataTable());

            _groceryListService = new GroceryListService();
        }

        [Fact]
        public void GetIngredientsForUser_ShouldReturnIngredients()
        {
            // Arrange
            var userId = 1;
            var mockTable = new DataTable();
            mockTable.Columns.Add("i_id", typeof(int));
            mockTable.Columns.Add("i_name", typeof(string));
            mockTable.Columns.Add("is_checked", typeof(bool));

            mockTable.Rows.Add(1, "Tomato", true);
            mockTable.Rows.Add(2, "Lettuce", false);

            // Mock ExecuteReader
            _mockDataLink.Setup(dl => dl.ExecuteReader(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
                         .Returns(mockTable);

            // Act
            var ingredients = _groceryListService.GetIngredientsForUser(userId);

            // Assert
            Assert.NotNull(ingredients);
            Assert.Equal(2, ingredients.Count);
            Assert.Equal("Tomato", ingredients[0].Name);
            Assert.Equal(1, ingredients[0].Id);
            Assert.True(ingredients[0].IsChecked);
        }

        [Fact]
        public void UpdateIsChecked_ShouldCallExecuteNonQuery()
        {
            // Arrange
            var userId = 1;
            var ingredientId = 2;
            var isChecked = true;

            // Act
            _groceryListService.UpdateIsChecked(userId, ingredientId, isChecked);

            // Assert
            _mockDataLink.Verify(dl => dl.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>()), Times.Once);
        }

        [Fact]
        public void AddIngredientToUser_ShouldSetIngredientId()
        {
            // Arrange
            var userId = 1;
            var ingredient = new GroceryIngredient { Name = "Carrot" };

            // Mock ExecuteScalar to return a new ID (e.g., 5)
            _mockDataLink.Setup(dl => dl.ExecuteScalar<int>(It.IsAny<string>(), It.IsAny<SqlParameter[]>(), It.IsAny<bool>()))
                         .Returns(5);

            // Act
            _groceryListService.AddIngredientToUser(userId, ingredient);

            // Assert
            Assert.Equal(5, ingredient.Id);  // The ID should be set to the mocked value
            _mockDataLink.Verify(dl => dl.ExecuteScalar<int>(It.IsAny<string>(), It.IsAny<SqlParameter[]>(), It.IsAny<bool>()), Times.Once);
        }
    }
}
