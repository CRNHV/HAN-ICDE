using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICDE.Data.Migrations
{
    /// <inheritdoc />
    public partial class OpdrachtFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpdrachtBeoordelingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cijfer = table.Column<double>(type: "float", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IngeleverdeOpdrachtId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpdrachtBeoordelingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpdrachtBeoordelingen_IngeleverdeOpdrachten_IngeleverdeOpdrachtId",
                        column: x => x.IngeleverdeOpdrachtId,
                        principalTable: "IngeleverdeOpdrachten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpdrachtBeoordelingen_IngeleverdeOpdrachtId",
                table: "OpdrachtBeoordelingen",
                column: "IngeleverdeOpdrachtId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpdrachtBeoordelingen");
        }
    }
}
