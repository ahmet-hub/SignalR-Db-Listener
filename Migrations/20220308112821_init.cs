using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalR_Db_Listener.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    HomeTeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeTeamScore = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwayTeamScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
