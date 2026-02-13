public class KitapCreateDto
{
    public string IsbnNo { get; set; } = null!;
    public string KitapAdi { get; set; } = null!;
    public int TurId { get; set; }
    public int SayfaSayisi { get; set; }
}
