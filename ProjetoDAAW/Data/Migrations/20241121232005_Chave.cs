using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDAAW.Data.Migrations
{
    /// <inheritdoc />
    public partial class Chave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Personagem_ArtistaId",
                table: "Personagem",
                column: "ArtistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Personagem_FilmeId",
                table: "Personagem",
                column: "FilmeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personagem_Artista_ArtistaId",
                table: "Personagem",
                column: "ArtistaId",
                principalTable: "Artista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personagem_Filme_FilmeId",
                table: "Personagem",
                column: "FilmeId",
                principalTable: "Filme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personagem_Artista_ArtistaId",
                table: "Personagem");

            migrationBuilder.DropForeignKey(
                name: "FK_Personagem_Filme_FilmeId",
                table: "Personagem");

            migrationBuilder.DropIndex(
                name: "IX_Personagem_ArtistaId",
                table: "Personagem");

            migrationBuilder.DropIndex(
                name: "IX_Personagem_FilmeId",
                table: "Personagem");
        }
    }
}
