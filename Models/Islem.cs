namespace KutuphaneApi.Models;

public class Islem
{
    public int Id { get; set; }                  // PK

    public int OgrenciId { get; set; }           // FK -> Ogrenci.Id
    public Ogrenci Ogrenci { get; set; } = null!;

    public int KopyaId { get; set; }             // FK -> KitapKopya.Id
    public KitapKopya Kopya { get; set; } = null!;

    public DateTime AlisTarihi { get; set; }
    public DateTime SonIadeTarihi { get; set; }

    public DateTime? IadeTarihi { get; set; }    // NULL olabilir ✅
}
