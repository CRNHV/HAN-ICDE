using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICDE.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBeoordelingCritereaRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BeoordelingCritereas_Opdrachten_OpdrachtId",
                table: "BeoordelingCritereas");

            migrationBuilder.DropIndex(
                name: "IX_BeoordelingCritereas_OpdrachtId",
                table: "BeoordelingCritereas");

            migrationBuilder.CreateTable(
                name: "BeoordelingCritereaOpdracht",
                columns: table => new
                {
                    BeoordelingCritereasId = table.Column<int>(type: "int", nullable: false),
                    OpdrachtId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeoordelingCritereaOpdracht", x => new { x.BeoordelingCritereasId, x.OpdrachtId });
                    table.ForeignKey(
                        name: "FK_BeoordelingCritereaOpdracht_BeoordelingCritereas_BeoordelingCritereasId",
                        column: x => x.BeoordelingCritereasId,
                        principalTable: "BeoordelingCritereas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeoordelingCritereaOpdracht_Opdrachten_OpdrachtId",
                        column: x => x.OpdrachtId,
                        principalTable: "Opdrachten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeoordelingCritereaOpdracht_OpdrachtId",
                table: "BeoordelingCritereaOpdracht",
                column: "OpdrachtId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeoordelingCritereaOpdracht");

            migrationBuilder.CreateIndex(
                name: "IX_BeoordelingCritereas_OpdrachtId",
                table: "BeoordelingCritereas",
                column: "OpdrachtId");

            migrationBuilder.AddForeignKey(
                name: "FK_BeoordelingCritereas_Opdrachten_OpdrachtId",
                table: "BeoordelingCritereas",
                column: "OpdrachtId",
                principalTable: "Opdrachten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
