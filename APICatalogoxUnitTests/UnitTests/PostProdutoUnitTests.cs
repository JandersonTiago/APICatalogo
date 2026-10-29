using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoxUnitTests.UnitTests;

public class PostProdutoUnitTests : IClassFixture<ProdutosUnitTestesController>
{
    private readonly ProdutosController _controller;

    public PostProdutoUnitTests(ProdutosUnitTestesController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    // Metodos de teste para POST
    [Fact]
    public async Task Post_Produto_Return_CreatedStatusCode()
    {
        // Arrange
        var novoProdutoDto = new ProdutoDTO
        {
            Nome = "Novo Produto",
            Descricao = "Descrição do Novo Produto",
            Preco = 10.99M,
            ImagemUrl = "imagemfake1.jpg",
            CategoriaId = 2
        };

        // Act
        var data = await _controller.Post(novoProdutoDto);

        // Assert
        var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
        createdResult.Subject.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task PostProduto_Return_BadRequest()
    {
        // Arrange
        ProdutoDTO prod = null;

        // Act
        var data = await _controller.Post(prod);

        // Assert
        var badRequestResult = data.Result.Should().BeOfType<BadRequestResult>();
        badRequestResult.Subject.StatusCode.Should().Be(400);
    }
}
