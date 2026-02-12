using Microsoft.EntityFrameworkCore;
using KutuphaneApi.Models;

namespace KutuphaneApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Islem> Islemler => Set<Islem>();
    public DbSet<Kitap> Kitaplar => Set<Kitap>();
    public DbSet<KitapKopya> KitapKopyalar => Set<KitapKopya>();
    public DbSet<KitapYazar> KitapYazarlar => Set<KitapYazar>();
    public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();
    public DbSet<Tur> Turler => Set<Tur>();
    public DbSet<Yazar> Yazarlar => Set<Yazar>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // UNIQUE: OgrNo
        modelBuilder.Entity<Ogrenci>()
            .HasIndex(o => o.OgrNo)
            .IsUnique();

        // UNIQUE: IsbnNo
        modelBuilder.Entity<Kitap>()
            .HasIndex(k => k.IsbnNo)
            .IsUnique();

        // N-N ara tablo: composite key
        modelBuilder.Entity<KitapYazar>()
            .HasKey(ky => new { ky.KitapId, ky.YazarId });

        base.OnModelCreating(modelBuilder);
    }
}
