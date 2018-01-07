using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ARSN.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competition",
                columns: table => new
                {
                    CompetitionID = table.Column<string>(nullable: false),
                    CompetitionBegin = table.Column<DateTime>(nullable: false),
                    CompetitionEnd = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OrganizerID = table.Column<string>(nullable: true),
                    SportType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competition", x => x.CompetitionID);
                });

            migrationBuilder.CreateTable(
                name: "Organizer",
                columns: table => new
                {
                    OrganizerID = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Organisation = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizer", x => x.OrganizerID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamID = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Organisation = table.Column<string>(nullable: true),
                    TrainerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameID = table.Column<string>(nullable: false),
                    AwayResult = table.Column<string>(nullable: true),
                    AwayTeamID = table.Column<string>(nullable: true),
                    CompetitionID = table.Column<string>(nullable: true),
                    HomeResult = table.Column<string>(nullable: true),
                    HomeTeamID = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Winner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_Game_Competition_CompetitionID",
                        column: x => x.CompetitionID,
                        principalTable: "Competition",
                        principalColumn: "CompetitionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_CompetitionID",
                table: "Game",
                column: "CompetitionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Organizer");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Competition");
        }
    }
}
