using Microsoft.AspNetCore.Mvc;
using TestAppASP.Controllers;
using TestAppASP.Models;


public class SearchControllerIntegrationTests 
{
    private readonly SController _controller;
    public SearchControllerIntegrationTests()
    {
        _controller = new SController();
    }

    // Создайте тест для проверки вывода результатов при успешном выполнении поиска по запросу
    [Fact]
    public async Task SearchAsync_WhenSearchRequest_ExpectRedirectToListAllSearchResults()
    {
        // Arrange
        var searchRequest = new SearchRequest { Request = "Test query" };

        // Act
        var result = await _controller.SearchAsync(searchRequest);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("ViewSearch", viewResult.ViewName);
    }
}
