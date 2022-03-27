using Microsoft.EntityFrameworkCore.Migrations;

namespace _220318_OS_RestaurantMVC.Migrations
{
    public partial class ilk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Malzemeler",
                columns: table => new
                {
                    MalzemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MalzemeAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Malzemeler", x => x.MalzemeId);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    UrunId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrunTanimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrunFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UrunResimURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.UrunId);
                    table.ForeignKey(
                        name: "FK_Urunler_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrunlerMalzemeler",
                columns: table => new
                {
                    MalzemeId = table.Column<int>(type: "int", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunlerMalzemeler", x => new { x.UrunId, x.MalzemeId });
                    table.ForeignKey(
                        name: "FK_UrunlerMalzemeler_Malzemeler_MalzemeId",
                        column: x => x.MalzemeId,
                        principalTable: "Malzemeler",
                        principalColumn: "MalzemeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrunlerMalzemeler_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "UrunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_KategoriId",
                table: "Urunler",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_UrunlerMalzemeler_MalzemeId",
                table: "UrunlerMalzemeler",
                column: "MalzemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrunlerMalzemeler");

            migrationBuilder.DropTable(
                name: "Malzemeler");

            migrationBuilder.DropTable(
                name: "Urunler");

            migrationBuilder.DropTable(
                name: "Kategoriler");
        }
    }
}
