using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisciplesClient_Update_Service.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateModTableToSoftware : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Software");

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftware",
                table: "Mods",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSoftware",
                table: "Mods");

            migrationBuilder.CreateTable(
                name: "Software",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    AuthorUserId = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FirstUpdateDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastUpdateDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Software_Users_AuthorUserId",
                        column: x => x.AuthorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Software_AuthorUserId",
                table: "Software",
                column: "AuthorUserId");
        }
    }
}
