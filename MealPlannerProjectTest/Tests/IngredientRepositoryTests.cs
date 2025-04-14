using Moq;
using Xunit;
using System.Data;
using System.Threading.Tasks;
using MealPlannerProject.Models;
using MealPlannerProject.Repositories;
using MealPlannerProject.Queries;

namespace MealPlannerProject.Tests
{
    public class IngredientRepositoryTests
    {
        private readonly Mock<DataLink> _mockDataLink;
        private readonly IngredientRepository _ingredientRepository;

        public IngredientRepositoryTests()
        {
            // Create mock DataLink
            _mockDataLink = new Mock<DataLink>();

            // Inject mock DataLink into IngredientRepository
            _ingredientRepository = new IngredientRepository();
        }

        [Fact]
        public async Task GetIngredientByNameAsync_ShouldReturnIngredient_WhenIngredientExists()
        {
            // Arrange
            var ingredientName = "Tomato";
            var mockTable = new DataTable();
            mockTable.Columns.Add("i_id", typeof(int));
            mockTable.Columns.Add("i_name", typeof(string));
            mockTable.Columns.Add("calories", typeof(float));
            mockTable.Columns.Add("protein", typeof(float));
            mockTable.Columns.Add("carbohydrates", typeof(float));
            mockTable.Columns.Add("fat", typeof(float));
            mockTable.Columns.Add("fiber", typeof(float));
            mockTable.Columns.Add("sugar", typeof(float));

            mockTable.Rows.Add(1, "Tomato", 22.5f, 1.1f, 4.5f, 0.2f, 3.0f, 3.6f);

            _mockDataLink.Setup(dl => dl.ExecuteSqlQuery(It.IsAny<string>(), It.IsAny<System.Data.SqlClient.SqlParameter[]?>())) 
                         .Returns(mockTable);

            // Act
            var ingredient = await _ingredientRepository.GetIngredientByNameAsync(ingredientName);

            // Assert
            Assert.NotNull(ingredient);
            Assert.Equal("Tomato", ingredient.Name);
            Assert.Equal(1, ingredient.Id);
            Assert.Equal(22.5f, ingredient.Calories);
        }

        [Fact]
        public async Task GetIngredientByNameAsync_ShouldReturnNull_WhenIngredientDoesNotExist()
        {
            // Arrange
            var ingredientName = "NonExistentIngredient";
            var mockTable = new DataTable();

            _mockDataLink.Setup(dl => dl.ExecuteSqlQuery(It.IsAny<string>(), It.IsAny<System.Data.SqlClient.SqlParameter[]?>())) 
                         .Returns(mockTable);

            // Act
            var ingredient = await _ingredientRepository.GetIngredientByNameAsync(ingredientName);

            // Assert
            Assert.Null(ingredient);
        }
    }
}
