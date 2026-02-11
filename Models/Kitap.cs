namespace KutuphaneApi.Models;

public class Kitap
{
    public int Id { get; set; }                      // PK
    public string IsbnNo { get; set; } = null!;      // UNIQUE
    public string KitapAdi { get; set; } = null!;

    public int TurId { get; set; }                   // FK -> Tur.Id
    public Tur Tur { get; set; } = null!;

    public int SayfaSayisi { get; set; }
    public int Puan { get; set; }

    // 1 kitap -> N kopya
    public List<KitapKopya> Kopyalar { get; set; } = new();

    // N-N (ara tablo)
    public List<KitapYazar> KitapYazarlar { get; set; } = new();
}
