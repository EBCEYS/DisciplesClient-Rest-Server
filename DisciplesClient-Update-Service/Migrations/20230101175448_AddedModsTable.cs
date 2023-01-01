using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisciplesClient_Update_Service.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AddedModsTable : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mods",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    FirstUpdateDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false),
                    AuthorUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mods", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Mods_Users_AuthorUserId",
                        column: x => x.AuthorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mods_AuthorUserId",
                table: "Mods",
                column: "AuthorUserId");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mods");
        }
    }
}
