using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_Application2.Migrations
{
    /// <inheritdoc />
    public partial class Renamethecorrectanswerinquestionmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswerId",
                table: "Questions");

            migrationBuilder.AddColumn<bool>(
                name: "CorrectAnswer",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
