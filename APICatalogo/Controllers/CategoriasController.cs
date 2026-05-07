using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<Categoria>>> Get()
    {
        var categorias =  await _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToListAsync();

        if (categorias is null)
        {
            return NotFound("Categorias não encontradas...");
        }

        return Ok(categorias);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProduto()
    {
        var categorias = await _context.Categorias.AsNoTracking().ToListAsync();

        if (categorias is null)
        {
            return NotFound("Categorias não encontradas...");
        }

        return Ok(categorias);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> Get(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada...");
        }

        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult> Post(Categoria categoria)
    {
        if (categoria is null)
        {
            return BadRequest("Categoria é nula...");
        }

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada...");
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return Ok(categoria);
    }
}
