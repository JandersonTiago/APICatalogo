using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoxUnitTests.UnitTests;

public class PutProdutoUnitTests : IClassFixture<ProdutosUnitTestesController>
{
    private readonly ProdutosController _controller;

    public PutProdutoUnitTests(ProdutosUnitTestesController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    // Testes de unidade para PUT
    [Fact]
    public async Task PutProduto_Return_OKResult()
    {
        // Arrange
        var prodId = 14;
        var UpdatedProdutoDto = new ProdutoDTO
        {
            ProdutoId = prodId,
            Nome = "Produto Atualizado - Teste",
            Descricao = "Minha Descrição",
            ImagemUrl = "imagem1.jpg",
            CategoriaId = 2
        };
        // Act
        var result = await _controller.Put(prodId, UpdatedProdutoDto) as ActionResult<ProdutoDTO>;

        // Assert
        result.Should().NotBeNull(); // Verifica se o resultado não é nulo
        result.Result.Should().BeOfType<OkObjectResult>(); // Verifica se o resultado é do tipo OkObjectResult
    }

    [Fact]
    public async Task PutProduto_Return_BadRequest()
    {
        // Arrange
        var prodId = 1000;

        var meuProduto = new ProdutoDTO
        {
            ProdutoId = 14, // Id diferente do parâmetro
            Nome = "Produto Atualizado - Teste",
            Descricao = "Minha Descrição alterada",
            ImagemUrl = "imagem11.jpg",
            CategoriaId = 2
        };

        // Act
        var result = await _controller.Put(prodId, meuProduto);

        // Assert
        result.Result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
    }
}
