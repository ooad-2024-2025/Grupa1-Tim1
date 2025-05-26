using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooadepazar.Migrations
{
    /// <inheritdoc />
    public partial class daaaaaaaaaaaaaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artikal_Korisnik_KorisnikId",
                table: "Artikal");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Artikal_ArtikalID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Korisnik_KorisnikID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Korisnik_KurirskaSluzbaID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifikacija_Korisnik_KorisnikId",
                table: "Notifikacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Pracenje_Korisnik_PraceniKorisnikID",
                table: "Pracenje");

            migrationBuilder.DropForeignKey(
                name: "FK_Pracenje_Korisnik_PratilacKorisnikID",
                table: "Pracenje");

            migrationBuilder.AddForeignKey(
                name: "FK_Artikal_Korisnik_KorisnikId",
                table: "Artikal",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Artikal_ArtikalID",
                table: "Narudzba",
                column: "ArtikalID",
                principalTable: "Artikal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Korisnik_KorisnikID",
                table: "Narudzba",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Korisnik_KurirskaSluzbaID",
                table: "Narudzba",
                column: "KurirskaSluzbaID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifikacija_Korisnik_KorisnikId",
                table: "Notifikacija",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pracenje_Korisnik_PraceniKorisnikID",
                table: "Pracenje",
                column: "PraceniKorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pracenje_Korisnik_PratilacKorisnikID",
                table: "Pracenje",
                column: "PratilacKorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artikal_Korisnik_KorisnikId",
                table: "Artikal");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Artikal_ArtikalID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Korisnik_KorisnikID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Korisnik_KurirskaSluzbaID",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifikacija_Korisnik_KorisnikId",
                table: "Notifikacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Pracenje_Korisnik_PraceniKorisnikID",
                table: "Pracenje");

            migrationBuilder.DropForeignKey(
                name: "FK_Pracenje_Korisnik_PratilacKorisnikID",
                table: "Pracenje");

            migrationBuilder.AddForeignKey(
                name: "FK_Artikal_Korisnik_KorisnikId",
                table: "Artikal",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Artikal_ArtikalID",
                table: "Narudzba",
                column: "ArtikalID",
                principalTable: "Artikal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Korisnik_KorisnikID",
                table: "Narudzba",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Korisnik_KurirskaSluzbaID",
                table: "Narudzba",
                column: "KurirskaSluzbaID",
                principalTable: "Korisnik",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifikacija_Korisnik_KorisnikId",
                table: "Notifikacija",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pracenje_Korisnik_PraceniKorisnikID",
                table: "Pracenje",
                column: "PraceniKorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pracenje_Korisnik_PratilacKorisnikID",
                table: "Pracenje",
                column: "PratilacKorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID");
        }
    }
}
