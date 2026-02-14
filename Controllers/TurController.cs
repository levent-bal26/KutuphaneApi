using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KutuphaneApi.Data;
using KutuphaneApi.Models;
using KutuphaneApi.Dtos.Tur;

namespace KutuphaneApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TurController : ControllerBase
{
    private readonly AppDbContext _context;

    public TurController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/tur
    [HttpGet]
    public async Task<ActionResult<List<TurReadDto>>> GetAll()
    {
        var turler = await _context.Turler
            .Select(t => new TurReadDto
            {
                Id = t.Id,
                TurAdi = t.TurAdi
            })
            .ToListAsync();

        return Ok(turler);
    }

    // GET: api/tur/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TurReadDto>> GetById(int id)
    {
        var tur = await _context.Turler.FindAsync(id);
        if (tur is null) return NotFound();

        return Ok(new TurReadDto { Id = tur.Id, TurAdi = tur.TurAdi });
    }

    // POST: api/tur
    [HttpPost]
    public async Task<ActionResult<TurReadDto>> Create(TurCreateDto dto)
    {
        var tur = new Tur { TurAdi = dto.TurAdi };

        _context.Turler.Add(tur);
        await _context.SaveChangesAsync();

        var result = new TurReadDto { Id = tur.Id, TurAdi = tur.TurAdi };
        return CreatedAtAction(nameof(GetById), new { id = tur.Id }, result);
    }
}
