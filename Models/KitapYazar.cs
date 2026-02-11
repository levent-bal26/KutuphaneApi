namespace KutuphaneApi.Models;

public class KitapYazar
{
    public int KitapId { get; set; }             // PK, FK -> Kitap.Id
    public Kitap Kitap { get; set; } = null!;

    public int YazarId { get; set; }             // PK, FK -> Yazar.Id
    public Yazar Yazar { get; set; } = null!;
}
