namespace KutuphaneApi.Models;

public class Tur
{
    public int Id { get; set; }                  // PK
    public string TurAdi { get; set; } = null!;

    // 1 tur -> N kitap
    public List<Kitap> Kitaplar { get; set; } = new();
}
