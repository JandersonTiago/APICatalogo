using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoxUnitTests.UnitTests;

public class GetProdutoUnitTests : IClassFixture<ProdutosUnitTestesController>
{
    private readonly ProdutosController _controller;

    public GetProdutoUnitTests(ProdutosUnitTestesController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task GetProdutoById_OKResult()
    {
        // Arrange
        var prodId = 2;

        // Act
        var data = await _controller.Get(prodId);

        // Assert (xUnit)
        //var okResult = Assert.IsType<OkObjectResult>(data.Result);
        //Assert.Equal(200, okResult.StatusCode);

        // Assert (FluentAssertions)
        data.Result.Should().BeOfType<OkObjectResult>() // Verifica se o resultado é do tipo OkObjectResult
            .Which.StatusCode.Should().Be(200); // Verifica se o código de status do OkObjectResult é 200
    }

    [Fact]
    public async Task GetProdutoById_Return_NotFound()
    {
        // Arrange
        var prodId = 999;

        // Act
        var data = await _controller.Get(prodId);

        // Assert
        data.Result.Should().BeOfType<NotFoundObjectResult>() // Verifica se o resultado é do tipo NotFoundObjectResult
            .Which.StatusCode.Should().Be(404); // Verifica se o código de status do NotFoundObjectResult é 404
    }

    [Fact]
    public async Task GetProdutoById_Return_BadRequest()
    {
        // Arrange
        var prodId = -1;

        // Act
        var data = await _controller.Get(prodId);

        // Assert
        data.Result.Should().BeOfType<BadRequestObjectResult>() // Verifica se o resultado é do tipo BadRequestObjectResult
            .Which.StatusCode.Should().Be(400); // Verifica se o código de status do BadRequestObjectResult é 400
    }

    [Fact]
    public async Task GetProdutos_Return_ListOfProdutosDTO()
    {
        // Act
        var data = await _controller.Get();

        // Assert
        data.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<IEnumerable<ProdutoDTO>>() // Verifica se o valor do OkObjectResult é atribuível a IEnumerable<ProdutoDTO>
            .And.NotBeNull();
    }

    [Fact]
    public async Task GetProdutos_Return_BadRequestResult()
    {
        // Act
        var data = await _controller.Get();

        // Assert
        data.Result.Should().BeOfType<BadRequestResult>();
    }
}
