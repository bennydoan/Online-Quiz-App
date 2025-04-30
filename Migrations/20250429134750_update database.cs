using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_Application2.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Questions");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answers");

            migrationBuilder.AddColumn<bool>(
                name: "CorrectAnswer",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
