using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICDE.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngeleverdeOpdrachten_Studenten_StudentNummer",
                table: "IngeleverdeOpdrachten");

            migrationBuilder.RenameColumn(
                name: "StudentNummer",
                table: "IngeleverdeOpdrachten",
                newName: "IngeleverdeOpdracht");

            migrationBuilder.RenameIndex(
                name: "IX_IngeleverdeOpdrachten_StudentNummer",
                table: "IngeleverdeOpdrachten",
                newName: "IX_IngeleverdeOpdrachten_IngeleverdeOpdracht");

            migrationBuilder.AddForeignKey(
                name: "FK_IngeleverdeOpdrachten_Studenten_IngeleverdeOpdracht",
                table: "IngeleverdeOpdrachten",
                column: "IngeleverdeOpdracht",
                principalTable: "Studenten",
                principalColumn: "StudentNummer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngeleverdeOpdrachten_Studenten_IngeleverdeOpdracht",
                table: "IngeleverdeOpdrachten");

            migrationBuilder.RenameColumn(
                name: "IngeleverdeOpdracht",
                table: "IngeleverdeOpdrachten",
                newName: "StudentNummer");

            migrationBuilder.RenameIndex(
                name: "IX_IngeleverdeOpdrachten_IngeleverdeOpdracht",
                table: "IngeleverdeOpdrachten",
                newName: "IX_IngeleverdeOpdrachten_StudentNummer");

            migrationBuilder.AddForeignKey(
                name: "FK_IngeleverdeOpdrachten_Studenten_StudentNummer",
                table: "IngeleverdeOpdrachten",
                column: "StudentNummer",
                principalTable: "Studenten",
                principalColumn: "StudentNummer");
        }
    }
}
