using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _repository;

    public ProdutosController(IProdutoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _repository.GetProdutos().ToList();

        if (produtos is null)
            return NotFound("Produtos não encontrados...");

        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _repository.GetProduto(id);

        if (produto is null)
            return NotFound($"Produto com id={id} não encontrada...");

        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest("Produto é nulo...");

        var produtoCriado = _repository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produtoCriado.ProdutoId }, produtoCriado);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
            return BadRequest();

        bool atualizado = _repository.Update(produto);

        if (atualizado)
            return Ok(produto);

        return StatusCode(500, $"Falha ao atualizar o produto com id={id}.");
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        bool deletado = _repository.Delete(id);

        if (deletado)
            return Ok($"Produto com id={id} deletado com sucesso.");

        return StatusCode(500, $"Falha ao deletar o produto com id={id}.");
    }
}
