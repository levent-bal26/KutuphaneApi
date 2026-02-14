using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KutuphaneApi.Data;
using KutuphaneApi.Models;
using KutuphaneApi.Dtos.Kitap;

namespace KutuphaneApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KitapController : ControllerBase
{
    private readonly AppDbContext _context;

    public KitapController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/kitap
    [HttpGet]
    public async Task<ActionResult<List<KitapReadDto>>> GetAll()
    {
        var list = await _context.Kitaplar
            .Include(k => k.Tur) // TurAdi döndürmek için
            .Select(k => new KitapReadDto
            {
                Id = k.Id,
                IsbnNo = k.IsbnNo,
                KitapAdi = k.KitapAdi,
                TurAdi = k.Tur.TurAdi
            })
            .ToListAsync();

        return Ok(list);
    }

    // GET: api/kitap/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<KitapReadDto>> GetById(int id)
    {
        var kitap = await _context.Kitaplar
            .Include(k => k.Tur)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (kitap is null) return NotFound();

        return Ok(new KitapReadDto
        {
            Id = kitap.Id,
            IsbnNo = kitap.IsbnNo,
            KitapAdi = kitap.KitapAdi,
            TurAdi = kitap.Tur.TurAdi
        });
    }

    // POST: api/kitap
    [HttpPost]
    public async Task<ActionResult> Create(KitapCreateDto dto)
    {
        // 1) TurId var mı?
        var turVarMi = await _context.Turler.AnyAsync(t => t.Id == dto.TurId);
        if (!turVarMi) return BadRequest("Geçersiz TurId.");

        // 2) ISBN unique mi? (çakışmayı önlemek için)
        var isbnVarMi = await _context.Kitaplar.AnyAsync(k => k.IsbnNo == dto.IsbnNo);
        if (isbnVarMi) return BadRequest("Bu ISBN zaten kayıtlı.");

        var kitap = new Kitap
        {
            IsbnNo = dto.IsbnNo,
            KitapAdi = dto.KitapAdi,
            TurId = dto.TurId,
            SayfaSayisi = dto.SayfaSayisi,
            Puan = 0 // DTO’da yoktu, sistemi korumak için burada set ediyoruz
        };

        _context.Kitaplar.Add(kitap);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = kitap.Id }, null);
    }
}
