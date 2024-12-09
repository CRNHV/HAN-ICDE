using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICDE.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyForOpleidingVakken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vakken_Opleidingen_OpleidingId",
                table: "Vakken");

            migrationBuilder.DropIndex(
                name: "IX_Vakken_OpleidingId",
                table: "Vakken");

            migrationBuilder.DropColumn(
                name: "OpleidingId",
                table: "Vakken");

            migrationBuilder.CreateTable(
                name: "OpleidingVak",
                columns: table => new
                {
                    OpleidingId = table.Column<int>(type: "int", nullable: false),
                    VakkenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpleidingVak", x => new { x.OpleidingId, x.VakkenId });
                    table.ForeignKey(
                        name: "FK_OpleidingVak_Opleidingen_OpleidingId",
                        column: x => x.OpleidingId,
                        principalTable: "Opleidingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpleidingVak_Vakken_VakkenId",
                        column: x => x.VakkenId,
                        principalTable: "Vakken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpleidingVak_VakkenId",
                table: "OpleidingVak",
                column: "VakkenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpleidingVak");

            migrationBuilder.AddColumn<int>(
                name: "OpleidingId",
                table: "Vakken",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vakken_OpleidingId",
                table: "Vakken",
                column: "OpleidingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vakken_Opleidingen_OpleidingId",
                table: "Vakken",
                column: "OpleidingId",
                principalTable: "Opleidingen",
                principalColumn: "Id");
        }
    }
}
