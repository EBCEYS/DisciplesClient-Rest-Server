using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisciplesClient_Update_Service.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateModTable : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Mods",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Mods");
        }
    }
}
