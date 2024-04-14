using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class init_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCompetitions_Competitions_CompetitionID",
                table: "StudentCompetitions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCompetitions_Students_StudentID",
                table: "StudentCompetitions");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCompetitions_Competitions_CompetitionID",
                table: "StudentCompetitions",
                column: "CompetitionID",
                principalTable: "Competitions",
                principalColumn: "CompetitionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCompetitions_Students_StudentID",
                table: "StudentCompetitions",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCompetitions_Competitions_CompetitionID",
                table: "StudentCompetitions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCompetitions_Students_StudentID",
                table: "StudentCompetitions");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCompetitions_Competitions_CompetitionID",
                table: "StudentCompetitions",
                column: "CompetitionID",
                principalTable: "Competitions",
                principalColumn: "CompetitionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCompetitions_Students_StudentID",
                table: "StudentCompetitions",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
