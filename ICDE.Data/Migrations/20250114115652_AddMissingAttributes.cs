using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICDE.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leeruitkomstsen_Cursussen_CursusId",
                table: "leeruitkomstsen");

            migrationBuilder.DropIndex(
                name: "IX_leeruitkomstsen_CursusId",
                table: "leeruitkomstsen");

            migrationBuilder.DropColumn(
                name: "CursusId",
                table: "leeruitkomstsen");

            migrationBuilder.DropColumn(
                name: "OpdrachtId",
                table: "BeoordelingCritereas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeadlineUtc",
                table: "Opdrachten",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LesInhoud",
                table: "Lessen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CursusMateriaal",
                table: "Cursussen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CursusLeeruitkomst",
                columns: table => new
                {
                    CursusId = table.Column<int>(type: "int", nullable: false),
                    LeeruitkomstenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursusLeeruitkomst", x => new { x.CursusId, x.LeeruitkomstenId });
                    table.ForeignKey(
                        name: "FK_CursusLeeruitkomst_Cursussen_CursusId",
                        column: x => x.CursusId,
                        principalTable: "Cursussen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursusLeeruitkomst_leeruitkomstsen_LeeruitkomstenId",
                        column: x => x.LeeruitkomstenId,
                        principalTable: "leeruitkomstsen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursusLeeruitkomst_LeeruitkomstenId",
                table: "CursusLeeruitkomst",
                column: "LeeruitkomstenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursusLeeruitkomst");

            migrationBuilder.DropColumn(
                name: "DeadlineUtc",
                table: "Opdrachten");

            migrationBuilder.DropColumn(
                name: "LesInhoud",
                table: "Lessen");

            migrationBuilder.DropColumn(
                name: "CursusMateriaal",
                table: "Cursussen");

            migrationBuilder.AddColumn<int>(
                name: "CursusId",
                table: "leeruitkomstsen",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpdrachtId",
                table: "BeoordelingCritereas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_leeruitkomstsen_CursusId",
                table: "leeruitkomstsen",
                column: "CursusId");

            migrationBuilder.AddForeignKey(
                name: "FK_leeruitkomstsen_Cursussen_CursusId",
                table: "leeruitkomstsen",
                column: "CursusId",
                principalTable: "Cursussen",
                principalColumn: "Id");
        }
    }
}
