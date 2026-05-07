using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> Get()
    {
        var produtos = await _context.Produtos.AsNoTracking().ToListAsync();

        if (produtos is null)
        {
            return NotFound("Produtos não encontrados..."); // 404 Not Found
        }
        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public async Task<ActionResult<Produto>> Get(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado..."); // 404 Not Found
        }
        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult> Post(Produto produto)
    {
        if (produto is null)
        {
            return BadRequest("Produto é nulo..."); // 400 Bad Request - erro de validação
        }

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(produto); // 200 OK - com conteúdo
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto não encontrado..."); // 404 Not Found
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return Ok(produto); // 200 OK - com conteúdo
    }
}
