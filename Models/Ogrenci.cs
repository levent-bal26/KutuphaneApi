namespace KutuphaneApi.Models;
public class Ogrenci
{
    public int Id { get; set; }                      // PK
    public string OgrNo { get; set; } = null!;       // UNIQUE

    public string OgrAd { get; set; } = null!;
    public string OgrSoyad { get; set; } = null!;
    public string Cinsiyet { get; set; } = null!;
    public DateTime DTarih { get; set; }
    public string Sinif { get; set; } = null!;
    public int Puan { get; set; }

    // Navigation: 1 ogrenci -> N islem
    public List<Islem> Islemler { get; set; } = new();
}
