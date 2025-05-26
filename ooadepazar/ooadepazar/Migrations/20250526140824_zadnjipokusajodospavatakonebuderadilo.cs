using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooadepazar.Migrations
{
    /// <inheritdoc />
    public partial class zadnjipokusajodospavatakonebuderadilo : Migration
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

            migrationBuilder.AddForeignKey(
                name: "FK_Artikal_Korisnik_KorisnikId",
                table: "Artikal",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Artikal_ArtikalID",
                table: "Narudzba",
                column: "ArtikalID",
                principalTable: "Artikal",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Korisnik_KorisnikID",
                table: "Narudzba",
                column: "KorisnikID",
                principalTable: "Korisnik",
                principalColumn: "ID");

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
                principalColumn: "ID");
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
        }
    }
}
