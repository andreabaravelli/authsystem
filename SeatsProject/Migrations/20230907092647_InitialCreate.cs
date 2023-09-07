using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatsProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prenotazioni",
                columns: table => new
                {
                    IdPrenotazine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Utente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodicePostazione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodicePrenotazione = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prenotazioni", x => x.IdPrenotazine);
                });

            migrationBuilder.CreateTable(
                name: "Sedi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodiceArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPadre = table.Column<int>(type: "int", nullable: true),
                    Prenotabile = table.Column<bool>(type: "bit", nullable: false),
                    ImmagineSvg = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedi", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prenotazioni");

            migrationBuilder.DropTable(
                name: "Sedi");
        }
    }
}
