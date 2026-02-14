using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KutuphaneApi.Data;
using KutuphaneApi.Models;
using KutuphaneApi.Dtos.Islem;

namespace KutuphaneApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IslemController : ControllerBase
{
    private readonly AppDbContext _context;

    public IslemController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/islem/odunc
    [HttpPost("odunc")]
    public async Task<ActionResult> OduncAl(IslemCreateDto dto)
    {
        // 1) Öğrenci var mı?
        var ogrenci = await _context.Ogrenciler.FindAsync(dto.OgrenciId);
        if (ogrenci is null) return BadRequest("Geçersiz OgrenciId.");

        // 2) Kopya var mı?
        var kopya = await _context.KitapKopyalar.FindAsync(dto.KopyaId);
        if (kopya is null) return BadRequest("Geçersiz KopyaId.");

        // 3) Kopya rafta mı?
        if (kopya.Durum != "Rafta")
            return BadRequest("Bu kitap kopyası şu an ödünçte.");

        // 4) İşlem oluştur
        var islem = new Islem
        {
            OgrenciId = dto.OgrenciId,
            KopyaId = dto.KopyaId,
            AlisTarihi = DateTime.UtcNow,
            SonIadeTarihi = DateTime.UtcNow.AddDays(7),
            IadeTarihi = null
        };

        // 5) Kopyanın durumunu güncelle (iş kuralı)
        kopya.Durum = "Oduncte";

        _context.Islemler.Add(islem);
        await _context.SaveChangesAsync();

        return Ok();
    }

    // POST: api/islem/iade/5
    [HttpPost("iade/{id:int}")]
    public async Task<ActionResult> IadeEt(int id)
    {
        var islem = await _context.Islemler.FindAsync(id);
        if (islem is null) return NotFound();

        if (islem.IadeTarihi is not null)
            return BadRequest("Bu işlem zaten iade edilmiş.");

        var kopya = await _context.KitapKopyalar.FindAsync(islem.KopyaId);
        if (kopya is null) return BadRequest("Kopya bulunamadı.");

        islem.IadeTarihi = DateTime.UtcNow;
        kopya.Durum = "Rafta";

        await _context.SaveChangesAsync();
        return Ok();
    }

    // GET: api/islem
    [HttpGet]
    public async Task<ActionResult<List<IslemReadDto>>> GetAll()
    {
        var list = await _context.Islemler
            .Include(i => i.Ogrenci)
            .Include(i => i.Kopya)
                .ThenInclude(k => k.Kitap)
            .Select(i => new IslemReadDto
            {
                Id = i.Id,
                OgrenciAdSoyad = i.Ogrenci.OgrAd + " " + i.Ogrenci.OgrSoyad,
                KitapAdi = i.Kopya.Kitap.KitapAdi,
                AlisTarihi = i.AlisTarihi,
                SonIadeTarihi = i.SonIadeTarihi,
                IadeTarihi = i.IadeTarihi
            })
            .ToListAsync();

        return Ok(list);
    }
}
