using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KutuphaneApi.Data;
using KutuphaneApi.Models;
using KutuphaneApi.Dtos.Yazar;

namespace KutuphaneApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class YazarController : ControllerBase
{
    private readonly AppDbContext _context;

    public YazarController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/yazar
    [HttpGet]
    public async Task<ActionResult<List<YazarReadDto>>> GetAll()
    {
        var yazarlar = await _context.Yazarlar
            .Select(y => new YazarReadDto
            {
                Id = y.Id,
                YazarAd = y.YazarAd,
                YazarSoyad = y.YazarSoyad
            })
            .ToListAsync();

        return Ok(yazarlar);
    }

    // GET: api/yazar/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<YazarReadDto>> GetById(int id)
    {
        var yazar = await _context.Yazarlar.FindAsync(id);
        if (yazar is null) return NotFound();

        return Ok(new YazarReadDto
        {
            Id = yazar.Id,
            YazarAd = yazar.YazarAd,
            YazarSoyad = yazar.YazarSoyad
        });
    }

    // POST: api/yazar
    [HttpPost]
    public async Task<ActionResult<YazarReadDto>> Create(YazarCreateDto dto)
    {
        var yazar = new Yazar
        {
            YazarAd = dto.YazarAd,
            YazarSoyad = dto.YazarSoyad
        };

        _context.Yazarlar.Add(yazar);
        await _context.SaveChangesAsync();

        var result = new YazarReadDto
        {
            Id = yazar.Id,
            YazarAd = yazar.YazarAd,
            YazarSoyad = yazar.YazarSoyad
        };

        return CreatedAtAction(nameof(GetById), new { id = yazar.Id }, result);
    }
}
