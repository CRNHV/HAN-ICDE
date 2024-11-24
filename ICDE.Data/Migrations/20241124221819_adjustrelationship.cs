using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICDE.Data.Migrations
{
    /// <inheritdoc />
    public partial class adjustrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leeruitkomstsen_BeoordelingCritereas_BeoordelingCritereaId",
                table: "leeruitkomstsen");

            migrationBuilder.DropIndex(
                name: "IX_leeruitkomstsen_BeoordelingCritereaId",
                table: "leeruitkomstsen");

            migrationBuilder.DropColumn(
                name: "BeoordelingCritereaId",
                table: "leeruitkomstsen");

            migrationBuilder.CreateTable(
                name: "BeoordelingCritereaLeeruitkomst",
                columns: table => new
                {
                    BeoordelingCritereaId = table.Column<int>(type: "int", nullable: false),
                    LeeruitkomstenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeoordelingCritereaLeeruitkomst", x => new { x.BeoordelingCritereaId, x.LeeruitkomstenId });
                    table.ForeignKey(
                        name: "FK_BeoordelingCritereaLeeruitkomst_BeoordelingCritereas_BeoordelingCritereaId",
                        column: x => x.BeoordelingCritereaId,
                        principalTable: "BeoordelingCritereas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeoordelingCritereaLeeruitkomst_leeruitkomstsen_LeeruitkomstenId",
                        column: x => x.LeeruitkomstenId,
                        principalTable: "leeruitkomstsen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeoordelingCritereaLeeruitkomst_LeeruitkomstenId",
                table: "BeoordelingCritereaLeeruitkomst",
                column: "LeeruitkomstenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeoordelingCritereaLeeruitkomst");

            migrationBuilder.AddColumn<int>(
                name: "BeoordelingCritereaId",
                table: "leeruitkomstsen",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_leeruitkomstsen_BeoordelingCritereaId",
                table: "leeruitkomstsen",
                column: "BeoordelingCritereaId");

            migrationBuilder.AddForeignKey(
                name: "FK_leeruitkomstsen_BeoordelingCritereas_BeoordelingCritereaId",
                table: "leeruitkomstsen",
                column: "BeoordelingCritereaId",
                principalTable: "BeoordelingCritereas",
                principalColumn: "Id");
        }
    }
}
