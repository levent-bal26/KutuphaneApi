namespace KutuphaneApi.Models;

public class Yazar
{
    public int Id { get; set; }                  // PK
    public string YazarAd { get; set; } = null!;
    public string YazarSoyad { get; set; } = null!;

    // N-N (ara tablo)
    public List<KitapYazar> KitapYazarlar { get; set; } = new();
}
