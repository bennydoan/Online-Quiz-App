using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_Application2.Migrations
{
    /// <inheritdoc />
    public partial class EdittheQuizReusltModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "percentage",
                table: "QuizResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "percentage",
                table: "QuizResults");
        }
    }
}
