using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechnicalTest_API.Controllers;
using TechnicalTest_API.DTO_s;
using TechnicalTest_API.Services;

public class SellsControllerTests
{
    private readonly Mock<ISellService> _mockSellService;
    private readonly SellsController _controller;

    public SellsControllerTests()
    {
        _mockSellService = new Mock<ISellService>();
        _controller = new SellsController(_mockSellService.Object);
    }

    [Fact]
    public async Task CreateSell_ValidSell_ReturnsCreatedResult()
    {
        var sellDto = new SellDto { ItemId = 1, Quantity = 2 };
        var newSell = new Sells { SellId = 1, ItemId = 1, Quantity = 2, Total = 20.00m };
        _mockSellService.Setup(service => service.CreateSellAsync(sellDto)).ReturnsAsync(newSell);

        var result = await _controller.CreateSell(sellDto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(newSell.SellId, createdResult?.RouteValues?["id"]);
        Assert.Equal(newSell, createdResult?.Value);
    }

    [Fact]
    public async Task CreateSell_InvalidSell_ReturnsBadRequest()
    {
        var sellDto = new SellDto { ItemId = 1, Quantity = 2 };
        _mockSellService?.Setup(service => service.CreateSellAsync(sellDto)).ReturnsAsync((Sells?)null);

        var result = await _controller.CreateSell(sellDto);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Invalid sell request.", badRequestResult.Value);
    }

    [Fact]
    public async Task GetSellById_SellExists_ReturnsOkResult()
    {
        var sellId = 1;
        var sell = new Sells { SellId = sellId, ItemId = 1, Quantity = 2, Total = 20.00m };
        _mockSellService.Setup(service => service.GetSellByIdAsync(sellId)).ReturnsAsync(sell);

        var result = await _controller.GetSellById(sellId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(sell, okResult.Value);
    }

    [Fact]
    public async Task GetSellById_SellDoesNotExist_ReturnsNotFound()
    {
        var sellId = 1;
        _mockSellService.Setup(service => service.GetSellByIdAsync(sellId)).ReturnsAsync((Sells?)null);

        var result = await _controller.GetSellById(sellId);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal($"Sell with ID {sellId} not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task DeleteSell_SellExists_ReturnsNoContent()
    {
        var sellId = 1;
        _mockSellService.Setup(service => service.DeleteSellAsync(sellId)).ReturnsAsync(true);

        var result = await _controller.DeleteSell(sellId);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteSell_SellDoesNotExist_ReturnsNotFound()
    {
        var sellId = 1;
        _mockSellService.Setup(service => service.DeleteSellAsync(sellId)).ReturnsAsync(false);

        var result = await _controller.DeleteSell(sellId);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal($"Sell with ID {sellId} not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task UpdateSell_ValidRequest_ReturnsNoContent()
    {
        var sellId = 1;
        var updatedSellDto = new SellDto { ItemId = 1, Quantity = 3 };
        _mockSellService.Setup(service => service.UpdateSellAsync(sellId, updatedSellDto)).ReturnsAsync(true);

        var result = await _controller.UpdateSell(sellId, updatedSellDto);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateSell_InvalidRequest_ReturnsBadRequest()
    {
        var sellId = 1;
        var updatedSellDto = new SellDto { ItemId = 1, Quantity = 3 };
        _mockSellService.Setup(service => service.UpdateSellAsync(sellId, updatedSellDto)).ReturnsAsync(false);

        var result = await _controller.UpdateSell(sellId, updatedSellDto);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid sell update request.", badRequestResult.Value);
    }

}
