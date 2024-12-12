using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICDE.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentNummer",
                table: "IngeleverdeOpdrachten",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Studenten",
                columns: table => new
                {
                    StudentNummer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenten", x => x.StudentNummer);
                    table.ForeignKey(
                        name: "FK_Studenten_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngeleverdeOpdrachten_StudentNummer",
                table: "IngeleverdeOpdrachten",
                column: "StudentNummer");

            migrationBuilder.CreateIndex(
                name: "IX_Studenten_UserId",
                table: "Studenten",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_IngeleverdeOpdrachten_Studenten_StudentNummer",
                table: "IngeleverdeOpdrachten",
                column: "StudentNummer",
                principalTable: "Studenten",
                principalColumn: "StudentNummer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngeleverdeOpdrachten_Studenten_StudentNummer",
                table: "IngeleverdeOpdrachten");

            migrationBuilder.DropTable(
                name: "Studenten");

            migrationBuilder.DropIndex(
                name: "IX_IngeleverdeOpdrachten_StudentNummer",
                table: "IngeleverdeOpdrachten");

            migrationBuilder.DropColumn(
                name: "StudentNummer",
                table: "IngeleverdeOpdrachten");
        }
    }
}
