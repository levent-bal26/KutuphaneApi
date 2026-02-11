namespace KutuphaneApi.Models;

public class KitapKopya
{
    public int Id { get; set; }                  // PK

    public int KitapId { get; set; }             // FK -> Kitap.Id
    
    public Kitap Kitap { get; set; } = null!;

    public string Durum { get; set; } = null!;
    public string RafKodu { get; set; } = null!;

    // 1 kopya -> N islem
    public List<Islem> Islemler { get; set; } = new();
}
