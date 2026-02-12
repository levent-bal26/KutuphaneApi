using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OgrNo = table.Column<string>(type: "TEXT", nullable: false),
                    OgrAd = table.Column<string>(type: "TEXT", nullable: false),
                    OgrSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    Cinsiyet = table.Column<string>(type: "TEXT", nullable: false),
                    DTarih = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Sinif = table.Column<string>(type: "TEXT", nullable: false),
                    Puan = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TurAdi = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Yazarlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    YazarAd = table.Column<string>(type: "TEXT", nullable: false),
                    YazarSoyad = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yazarlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kitaplar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsbnNo = table.Column<string>(type: "TEXT", nullable: false),
                    KitapAdi = table.Column<string>(type: "TEXT", nullable: false),
                    TurId = table.Column<int>(type: "INTEGER", nullable: false),
                    SayfaSayisi = table.Column<int>(type: "INTEGER", nullable: false),
                    Puan = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitaplar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kitaplar_Turler_TurId",
                        column: x => x.TurId,
                        principalTable: "Turler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KitapKopyalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KitapId = table.Column<int>(type: "INTEGER", nullable: false),
                    Durum = table.Column<string>(type: "TEXT", nullable: false),
                    RafKodu = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitapKopyalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KitapKopyalar_Kitaplar_KitapId",
                        column: x => x.KitapId,
                        principalTable: "Kitaplar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KitapYazarlar",
                columns: table => new
                {
                    KitapId = table.Column<int>(type: "INTEGER", nullable: false),
                    YazarId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitapYazarlar", x => new { x.KitapId, x.YazarId });
                    table.ForeignKey(
                        name: "FK_KitapYazarlar_Kitaplar_KitapId",
                        column: x => x.KitapId,
                        principalTable: "Kitaplar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KitapYazarlar_Yazarlar_YazarId",
                        column: x => x.YazarId,
                        principalTable: "Yazarlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Islemler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OgrenciId = table.Column<int>(type: "INTEGER", nullable: false),
                    KopyaId = table.Column<int>(type: "INTEGER", nullable: false),
                    AlisTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SonIadeTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IadeTarihi = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islemler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Islemler_KitapKopyalar_KopyaId",
                        column: x => x.KopyaId,
                        principalTable: "KitapKopyalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Islemler_Ogrenciler_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrenciler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Islemler_KopyaId",
                table: "Islemler",
                column: "KopyaId");

            migrationBuilder.CreateIndex(
                name: "IX_Islemler_OgrenciId",
                table: "Islemler",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_KitapKopyalar_KitapId",
                table: "KitapKopyalar",
                column: "KitapId");

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_IsbnNo",
                table: "Kitaplar",
                column: "IsbnNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_TurId",
                table: "Kitaplar",
                column: "TurId");

            migrationBuilder.CreateIndex(
                name: "IX_KitapYazarlar_YazarId",
                table: "KitapYazarlar",
                column: "YazarId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_OgrNo",
                table: "Ogrenciler",
                column: "OgrNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Islemler");

            migrationBuilder.DropTable(
                name: "KitapYazarlar");

            migrationBuilder.DropTable(
                name: "KitapKopyalar");

            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "Yazarlar");

            migrationBuilder.DropTable(
                name: "Kitaplar");

            migrationBuilder.DropTable(
                name: "Turler");
        }
    }
}
