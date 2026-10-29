using APICatalogo.Controllers;
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
}
