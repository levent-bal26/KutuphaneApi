namespace KutuphaneApi.Dtos.Ogrenci;
public class OgrenciReadDto
{
    public int Id { get; set; }
    public string OgrNo { get; set; } = null!;
    public string OgrAd { get; set; } = null!;
    public string OgrSoyad { get; set; } = null!;
    public int Puan { get; set; }
}
