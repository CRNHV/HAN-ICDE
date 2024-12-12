using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICDE.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentEntity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngeleverdeOpdrachten_Studenten_IngeleverdeOpdracht",
                table: "IngeleverdeOpdrachten");

            migrationBuilder.DropIndex(
                name: "IX_IngeleverdeOpdrachten_IngeleverdeOpdracht",
                table: "IngeleverdeOpdrachten");

            migrationBuilder.DropColumn(
                name: "IngeleverdeOpdracht",
                table: "IngeleverdeOpdrachten");

            migrationBuilder.AddColumn<int>(
                name: "StudentNummer",
                table: "IngeleverdeOpdrachten",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IngeleverdeOpdrachten_StudentNummer",
                table: "IngeleverdeOpdrachten",
                column: "StudentNummer");

            migrationBuilder.AddForeignKey(
                name: "FK_IngeleverdeOpdrachten_Studenten_StudentNummer",
                table: "IngeleverdeOpdrachten",
                column: "StudentNummer",
                principalTable: "Studenten",
                principalColumn: "StudentNummer",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngeleverdeOpdrachten_Studenten_StudentNummer",
                table: "IngeleverdeOpdrachten");

            migrationBuilder.DropIndex(
                name: "IX_IngeleverdeOpdrachten_StudentNummer",
                table: "IngeleverdeOpdrachten");

            migrationBuilder.DropColumn(
                name: "StudentNummer",
                table: "IngeleverdeOpdrachten");

            migrationBuilder.AddColumn<int>(
                name: "IngeleverdeOpdracht",
                table: "IngeleverdeOpdrachten",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngeleverdeOpdrachten_IngeleverdeOpdracht",
                table: "IngeleverdeOpdrachten",
                column: "IngeleverdeOpdracht");

            migrationBuilder.AddForeignKey(
                name: "FK_IngeleverdeOpdrachten_Studenten_IngeleverdeOpdracht",
                table: "IngeleverdeOpdrachten",
                column: "IngeleverdeOpdracht",
                principalTable: "Studenten",
                principalColumn: "StudentNummer");
        }
    }
}
