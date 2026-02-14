using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KutuphaneApi.Data;
using KutuphaneApi.Models;
using KutuphaneApi.Dtos.KitapKopya;

namespace KutuphaneApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KitapKopyaController : ControllerBase
{
    private readonly AppDbContext _context;

    public KitapKopyaController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/kitapkopya
    [HttpGet]
    public async Task<ActionResult<List<KitapKopyaReadDto>>> GetAll()
    {
        var list = await _context.KitapKopyalar
            .Include(k => k.Kitap) // KitapAdi döndürmek için
            .Select(k => new KitapKopyaReadDto
            {
                Id = k.Id,
                KitapAdi = k.Kitap.KitapAdi,
                Durum = k.Durum,
                RafKodu = k.RafKodu
            })
            .ToListAsync();

        return Ok(list);
    }

    // POST: api/kitapkopya
    [HttpPost]
    public async Task<ActionResult> Create(KitapKopyaCreateDto dto)
    {
        // KitapId var mı?
        var kitapVarMi = await _context.Kitaplar.AnyAsync(x => x.Id == dto.KitapId);
        if (!kitapVarMi) return BadRequest("Geçersiz KitapId.");

        var kopya = new KitapKopya
        {
            KitapId = dto.KitapId,
            RafKodu = dto.RafKodu,
            Durum = "Rafta" // yeni eklenen kopya rafta başlar
        };

        _context.KitapKopyalar.Add(kopya);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
