using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KutuphaneApi.Data;
using KutuphaneApi.Models;
using KutuphaneApi.Dtos.Ogrenci;

namespace KutuphaneApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OgrenciController : ControllerBase
{
    private readonly AppDbContext _context;

    public OgrenciController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/ogrenci
    [HttpGet]
    public async Task<ActionResult<List<OgrenciReadDto>>> GetAll()
    {
        var list = await _context.Ogrenciler
            .Select(o => new OgrenciReadDto
            {
                Id = o.Id,
                OgrNo = o.OgrNo,
                OgrAd = o.OgrAd,
                OgrSoyad = o.OgrSoyad,
                Puan = o.Puan
            })
            .ToListAsync();

        return Ok(list);
    }

    // POST: api/ogrenci
    [HttpPost]
    public async Task<ActionResult> Create(OgrenciCreateDto dto)
    {
        // OgrNo unique kontrolü
        var ogrNoVarMi = await _context.Ogrenciler.AnyAsync(o => o.OgrNo == dto.OgrNo);
        if (ogrNoVarMi) return BadRequest("Bu öğrenci numarası zaten kayıtlı.");

        var ogr = new Ogrenci
        {
            OgrNo = dto.OgrNo,
            OgrAd = dto.OgrAd,
            OgrSoyad = dto.OgrSoyad,
            Sinif = dto.Sinif,
            // Entity’de Cinsiyet/DTarih var ama CreateDto’da yok: istersen CreateDto’ya eklersin
            Cinsiyet = "Bilinmiyor",
            DTarih = DateTime.UtcNow.Date,
            Puan = 0
        };

        _context.Ogrenciler.Add(ogr);
        await _context.SaveChangesAsync();

        return Ok();
    }

    // PUT: api/ogrenci/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, OgrenciUpdateDto dto)
    {
        var ogr = await _context.Ogrenciler.FindAsync(id);
        if (ogr is null) return NotFound();

        ogr.Sinif = dto.Sinif;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
