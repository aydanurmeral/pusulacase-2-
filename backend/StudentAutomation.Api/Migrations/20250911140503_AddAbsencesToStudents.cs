using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAutomation.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAbsencesToStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Absences",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Absences",
                table: "Students");
        }
    }
}
