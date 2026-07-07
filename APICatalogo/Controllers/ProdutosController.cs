using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IRepository<Produto> _repository;

    public ProdutosController(IProdutoRepository produtoRepository, IRepository<Produto> repository)
    {
        _produtoRepository = produtoRepository;
        _repository = repository;
    }

    [HttpGet("produto/{id}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
    {
        var produtos = _produtoRepository.GetProdutosPorCategoria(id);

        if (produtos is null)
            return NotFound($"Produtos da categoria com id={id} não encontrados...");

        return Ok(produtos);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _repository.GetAll();

        if (produtos is null)
            return NotFound("Produtos não encontrados...");

        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _repository.Get(p => p.ProdutoId == id);

        if (produto is null)
            return NotFound($"Produto com id={id} não encontrada...");

        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest("Produto é nulo...");

        var novoProduto = _repository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
            return BadRequest();

        var produtoAtualizado = _repository.Update(produto);

        if (produtoAtualizado is null)
            return BadRequest("Falha ao atualizar o produto.");

        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _repository.Get(p => p.ProdutoId == id);

        if (produto is null)
            return NotFound($"Produto com id={id} não encontrado...");

        var produtoDeletado = _repository.Delete(produto);

        if (produtoDeletado is null)
            return BadRequest("Falha ao deletar o produto.");

        return Ok(produtoDeletado);
    }
}
