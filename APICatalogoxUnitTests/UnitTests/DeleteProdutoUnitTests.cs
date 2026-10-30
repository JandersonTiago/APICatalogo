using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoxUnitTests.UnitTests;

public class DeleteProdutoUnitTests : IClassFixture<ProdutosUnitTestesController>
{
    private readonly ProdutosController _controller;

    public DeleteProdutoUnitTests(ProdutosUnitTestesController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    // Teste para o DELETE
    [Fact]
    public async Task DeleteProdutoById_Return_OkResult()
    {
        // Arrange
        var prodId = 2;

        // Act
        var result = await _controller.Delete(prodId);

        // Assert
        result.Should().NotBeNull(); // Verifica se o resultado não é nulo
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task DeleteProdutoById_Return_NotFoundResult()
    {
        // Arrange
        var prodId = 999; // ID que não existe

        // Act
        var result = await _controller.Delete(prodId) as ActionResult<ProdutoDTO>;

        // Assert
        result.Should().NotBeNull(); // Verifica se o resultado não é nulo
        result.Result.Should().BeOfType<NotFoundObjectResult>();
    }
}
