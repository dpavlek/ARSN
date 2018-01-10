using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ARSN.Migrations
{
    public partial class DBVersion9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizer",
                columns: table => new
                {
                    OrganizerID = table.Column<Guid>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Organisation = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizer", x => x.OrganizerID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamID = table.Column<Guid>(nullable: false),
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
                name: "Competition",
                columns: table => new
                {
                    CompetitionID = table.Column<Guid>(nullable: false),
                    CompetitionBegin = table.Column<DateTime>(nullable: false),
                    CompetitionEnd = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OrganizerID = table.Column<Guid>(nullable: true),
                    SportType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competition", x => x.CompetitionID);
                    table.ForeignKey(
                        name: "FK_Competition_Organizer_OrganizerID",
                        column: x => x.OrganizerID,
                        principalTable: "Organizer",
                        principalColumn: "OrganizerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameID = table.Column<Guid>(nullable: false),
                    AwayResult = table.Column<string>(nullable: true),
                    AwayTeamTeamID = table.Column<Guid>(nullable: true),
                    CompetitionID = table.Column<Guid>(nullable: true),
                    HomeResult = table.Column<string>(nullable: true),
                    HomeTeamTeamID = table.Column<Guid>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Winner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_AwayGame",
                        column: x => x.AwayTeamTeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_Competition_CompetitionID",
                        column: x => x.CompetitionID,
                        principalTable: "Competition",
                        principalColumn: "CompetitionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeGame",
                        column: x => x.HomeTeamTeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competition_OrganizerID",
                table: "Competition",
                column: "OrganizerID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_AwayTeamTeamID",
                table: "Game",
                column: "AwayTeamTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_CompetitionID",
                table: "Game",
                column: "CompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_HomeTeamTeamID",
                table: "Game",
                column: "HomeTeamTeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Competition");

            migrationBuilder.DropTable(
                name: "Organizer");
        }
    }
}
