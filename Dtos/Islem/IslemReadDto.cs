namespace KutuphaneApi.Dtos.Islem;
public class IslemReadDto
{
    public int Id { get; set; }
    public string OgrenciAdSoyad { get; set; } = null!;
    public string KitapAdi { get; set; } = null!;
    public DateTime AlisTarihi { get; set; }
    public DateTime SonIadeTarihi { get; set; }
    public DateTime? IadeTarihi { get; set; }
}
